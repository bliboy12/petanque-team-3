import {ReactNode} from "react";

interface ButtonProps {
  children?: ReactNode;
  onClick?: () => void;
}

export default function Button({ children, onClick }: ButtonProps ) {
  return (
    <button
      onClick={onClick}
      className="mt-0 mb-2 bg-[#ccac4c] hover:bg-[#b8953d] text-white font-bold px-6 py-3 rounded-xl transition cursor-pointer"
    >
      {children}
    </button>
  )
}