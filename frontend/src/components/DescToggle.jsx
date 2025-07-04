import Toggle from './Toggle'; // Ensure correct path to your Toggle component

export default function DescToggle({ title, description, checked, onChange }) {
  return (
    <div className="mt-10 mb-10 flex items-center justify-between">
        <div>
            <p className="text-md">{title}</p>
            <p className="text-sm text-[#5E5E5E]">
            {description}
            </p>
        </div>
      <Toggle checked={checked} onChange={() => onChange(!checked)} /> {/* Pass toggled value to onChange */}
    </div>
  );
}