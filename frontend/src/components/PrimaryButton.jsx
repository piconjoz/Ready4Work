export default function PrimaryButton({
  label = "Submit",
  type = "button",
  onClick = () => {},
  className = "",
}) {
  return (
    <button
      type={type}
      onClick={onClick}
      className={`bg-stone-950 text-white rounded-lg py-3 px-4 w-full hover:bg-stone-800 transition ${className}`}
    >
      {label}
    </button>
  );
}