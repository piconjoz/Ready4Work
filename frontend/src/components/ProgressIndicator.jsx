import { MdCheck, MdPerson, MdAssignment } from "react-icons/md";

const steps = [
  {
    icon: <MdCheck className="text-blue-600" size={24} />,
    label: "Done",
    active: true,
    color: "bg-blue-100"
  },
  {
    icon: <MdPerson className="text-gray-400" size={24} />,
    label: "Profile",
    active: false,
    color: "bg-gray-100"
  },
  {
    icon: <MdAssignment className="text-gray-400" size={24} />,
    label: "Submit",
    active: false,
    color: "bg-gray-100"
  }
];

export default function ProgressSteps() {
  return (
    <div className="w-full mx-auto py-4">
      <div className="flex items-center justify-between relative">
        {steps.map((step, i) => (
          <div className="flex flex-col items-center flex-1 relative" key={step.label}>
            {/* Line to previous (except first) */}
            {i !== 0 && (
              <div
                className={`absolute top-1/3 left-0 -translate-y-1/2 w-1/2 h-1
                  ${steps[i - 1].active ? "bg-blue-100" : "bg-gray-200"}
                  z-0`}
              />
            )}
            {/* Line to next (except last) */}
            {i !== steps.length - 1 && (
              <div
                className={`absolute top-1/3 right-0 -translate-y-1/2 w-1/2 h-1
                  ${step.active ? "bg-blue-100" : "bg-gray-200"}
                  z-0`}
              />
            )}
            <div
              className={`w-15 h-15 rounded-full flex items-center justify-center mb-2 ${step.color} z-10`}
            >
              {step.icon}
            </div>
            <span className="mt-1 text-md font-medium text-[#2b2f35]">{step.label}</span>
          </div>
        ))}
      </div>
    </div>
  );
}