import { useEffect, useState } from "react";
import Kalender from '../Components/Kalender.tsx';
import PageHeader from "../../components/PageHeader.tsx";
import Button from "../../components/buttons/Button.tsx";
import Weather from '../Components/Weather.tsx';

const apiUrl = import.meta.env.VITE_API_URL;

interface Speeldag {
  speeldagId: number;
  datum: string;
}

export default function Dagklassementpagina() {
  const [speeldagen, setSpeeldagen] = useState<Speeldag[]>([]);
  const [selectedSpeeldag, setSelectedSpeeldag] = useState<Speeldag | null>(null);
  const [pdfUrl, setPdfUrl] = useState<string | null>(() => localStorage.getItem('dagklassementPdfUrl') || null);
  const [showCalendar, setShowCalendar] = useState(false);

  useEffect(() => {
    const fetchSpeeldagen = async () => {
      try {
        const response = await fetch(`${apiUrl}/speeldagen`);
        if (!response.ok) throw new Error("Fout bij ophalen van speeldagen");
        const data: Speeldag[] = await response.json();
        setSpeeldagen(data);

        const savedSpeeldagId = localStorage.getItem('speeldagId');
        if (savedSpeeldagId) {
            const foundSpeeldag = data.find((dag) => dag.speeldagId.toString() === savedSpeeldagId);
            setSelectedSpeeldag(foundSpeeldag || null);
        }
      } catch (error) {
        console.error("Fout bij laden van speeldagen:", error);
        alert("Kon speeldagen niet laden.");
      }
    };

    fetchSpeeldagen();
  }, []);

  const fetchPdf = async () => {
    if (!selectedSpeeldag) {
      alert("Selecteer een speeldag.");
      return;
    }

    localStorage.setItem('speeldagId', selectedSpeeldag.speeldagId.toString());

    // Reset bestaande PDF
    setPdfUrl(null);
    localStorage.removeItem('dagklassementPdfUrl');

    try {
      const response = await fetch(
        `${apiUrl}/pdf/dailyrankings/${selectedSpeeldag.speeldagId}`,
        {
          method: "POST",
          headers: {
              Accept: "application/pdf",
          },
        }
      );

      if (!response.ok) throw new Error("Fout bij ophalen van PDF");

      const blob = await response.blob();
      const reader = new FileReader();

      reader.onloadend = () => {
        const base64data = reader.result as string;
        setPdfUrl(base64data);
        localStorage.setItem('dagklassementPdfUrl', base64data);
      };

      reader.readAsDataURL(blob);
    } catch (error) {
      console.error("Fout bij ophalen van PDF:", error);
      alert("Kon PDF niet ophalen.");
    }
  };

  const handleSelectSpeeldag = (speeldag: Speeldag) => {
    setSelectedSpeeldag(speeldag);
    localStorage.setItem('speeldagId', speeldag.speeldagId.toString());
    setShowCalendar(false);
  };

  const handleToggleCalendar = () => {
    setShowCalendar(!showCalendar);
  };

  return (
    <div className="p-0 max-w-3xl mx-auto">
      <PageHeader title="Dagklassement"/>
        {speeldagen.length > 0 && (
          <Kalender
            speeldagen={speeldagen}
            selectedSpeeldag={selectedSpeeldag}
            onSelectSpeeldag={handleSelectSpeeldag}
            showCalendar={showCalendar}
            onToggleCalendar={handleToggleCalendar}
          />
        )}

          <Weather speeldagDatum={selectedSpeeldag?.datum ?? null} />

      <Button onClick={fetchPdf}>
        (Her)genereer PDF
      </Button>

      {pdfUrl && selectedSpeeldag && (
        <div className="mt-6">
          <iframe
            src={pdfUrl}
            width="100%"
            height="600px"
            title="Dagklassement PDF"
            className="border rounded mb-4"
          ></iframe>

          <a
            href={pdfUrl}
            download={`dagklassement-speeldag-${selectedSpeeldag.speeldagId}-${selectedSpeeldag.datum}.pdf`}
            className="bg-[#fbd46d] text-[#3c444c] font-bold py-2 px-4 rounded hover:bg-[#f7c84c] transition cursor-pointer block mx-auto"
          >
              Download PDF
          </a>
        </div>
      )}
    </div>
  );
}
