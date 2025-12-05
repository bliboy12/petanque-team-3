import { useState, useEffect } from 'react';

const apiUrl = import.meta.env.VITE_API_URL;

interface WeatherData {
    temperature: number;
    precipitation: number;
    location: string;
    date: string;
}

interface WeatherProps {
    speeldagDatum: string | null;
}

// Default to Ghent, Belgium
const DEFAULT_LATITUDE = 51.05;
const DEFAULT_LONGITUDE = 3.72;

function Weather({ speeldagDatum }: WeatherProps) {
    const [weather, setWeather] = useState<WeatherData | null>(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [location, setLocation] = useState<{ latitude: number; longitude: number } | null>(null);

    // Get browser location on component mount
    useEffect(() => {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    setLocation({
                        latitude: position.coords.latitude,
                        longitude: position.coords.longitude
                    });
                },
                () => {
                    // Error or denied - use default location (Ghent)
                    setLocation({
                        latitude: DEFAULT_LATITUDE,
                        longitude: DEFAULT_LONGITUDE
                    });
                }
            );
        } else {
            // Geolocation not supported - use default location
            setLocation({
                latitude: DEFAULT_LATITUDE,
                longitude: DEFAULT_LONGITUDE
            });
        }
    }, []);

    // Fetch weather when speeldag or location changes
    useEffect(() => {
        if (!speeldagDatum || !location) {
            // Clear weather data when no date is selected
            setWeather(null);
            return;
        }

        const fetchWeather = async () => {
            setLoading(true);
            setError(null);
            // Clear old weather data when fetching new data
            setWeather(null);

            try {
                const dateObj = new Date(speeldagDatum);
                const dateString = dateObj.toISOString().split('T')[0];

                // Ensure API URL includes /api prefix
                const weatherApiUrl = apiUrl.endsWith('/api') 
                    ? `${apiUrl}/weather/forecast` 
                    : `${apiUrl}/api/weather/forecast`;

                const response: Response = await fetch(
                    `${weatherApiUrl}?date=${dateString}&latitude=${location.latitude}&longitude=${location.longitude}`
                );

                if (!response.ok) {
                    throw new Error('Kon weerdata niet ophalen');
                }

                const data: any = await response.json();
                
                // Handle both camelCase and PascalCase property names
                const temperature = data.temperature ?? data.Temperature;
                const precipitation = data.precipitation ?? data.Precipitation;
                const locationName = data.location ?? data.Location ?? '';
                const dateStr = data.date ?? data.Date ?? '';
                
                if (temperature === undefined || precipitation === undefined) {
                    throw new Error('Onvolledige weerdata ontvangen');
                }
                
                setWeather({
                    temperature: temperature,
                    precipitation: precipitation,
                    location: locationName,
                    date: dateStr
                });
            } catch (err) {
                setError(err instanceof Error ? err.message : 'Onbekende fout');
            } finally {
                setLoading(false);
            }
        };

        fetchWeather();
    }, [speeldagDatum, location]);

    if (!speeldagDatum) {
        return null;
    }

    return (
        <div className="bg-white border border-[#fbd46d] shadow-lg rounded-2xl p-6 mb-6">
            <h3 className="text-xl font-bold text-[#3c444c] mb-4">Weersvoorspelling</h3>
            
            {loading && (
                <p className="text-gray-600">Weerdata ophalen...</p>
            )}

            {error && (
                <p className="text-red-600">Fout: {error}</p>
            )}

            {weather && !loading && !error && (
                <div className="space-y-2">
                    <div className="flex justify-between items-center">
                        <span className="text-gray-700">Temperatuur:</span>
                        <span className="text-lg font-semibold text-[#3c444c]">
                            {weather.temperature.toFixed(1)}°C
                        </span>
                    </div>
                    <div className="flex justify-between items-center">
                        <span className="text-gray-700">Neerslag:</span>
                        <span className="text-lg font-semibold text-[#3c444c]">
                            {weather.precipitation.toFixed(1)} mm
                        </span>
                    </div>
                    <div className="text-sm text-gray-500 mt-2 pt-2 border-t">
                        Locatie: {location?.latitude === DEFAULT_LATITUDE && location?.longitude === DEFAULT_LONGITUDE 
                            ? 'Gent, België (standaard)' 
                            : `Lat: ${location?.latitude.toFixed(2)}, Lon: ${location?.longitude.toFixed(2)}`}
                    </div>
                </div>
            )}
        </div>
    );
}

export default Weather;

