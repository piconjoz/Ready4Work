export default function InfoCard({ title, info = [] }) {
  return (
    <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6">
      <h2 className="text-lg font-semibold mb-3">{title}</h2>
      <div className="space-y-3">
        {info.map(({ label, value }, idx) => (
          <div key={idx} className="flex justify-between items-center">
            <span className="text-sm underline">{label}</span>
            <span className="text-sm text-right break-words max-w-[60%]">{value}</span>
          </div>
        ))}
      </div>
    </div>
  );
}