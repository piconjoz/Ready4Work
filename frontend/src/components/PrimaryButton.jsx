export default function PrimaryButton({
  label = "Submit",
  type = "button",
  onClick = () => {},
  className = "",
  disabled = false,
}) {
  return (
    <button
      type={type}
      onClick={onClick}
      disabled={disabled}
      className={`rounded-lg py-3 px-4 w-full transition ${
        disabled
          ? "bg-gray-300 text-gray-500 cursor-not-allowed"
          : "bg-stone-950 text-white hover:bg-stone-800"
      } ${className}`}
    >
      {label}
    </button>
  );
}
