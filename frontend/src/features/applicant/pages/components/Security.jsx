import StatusInputField from '../../../../components/StatusInputField';
import DescToggle from '../../../../components/DescToggle';
import { useState } from "react";
import SelectField from '../../../../components/SelectField';


export default function Security() {

const [selected, setSelected] = useState("0000");

const timeoutOptions = [
    { value: "0000", label: "5 Min" },
    { value: "0100", label: "10 Min" },
    { value: "0200", label: "15 Min" },
    { value: "0300", label: "1 Day" },
    { value: "2300", label: "1 Month" },
];

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3  gap-6 md:gap-2 mt-6">
      {/* Left Section: Change Password */}
      <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6">
        <h2 className="text-lg font-semibold mb-4">Change Password</h2>
        <StatusInputField
          label="Current Password"
          name="currentPassword"
          type="password"
          status="default"
          errorMessage=""
        />
        <StatusInputField
          label="New Password"
          name="newPassword"
          type="password"
          status="error"
          errorMessage="Password does not meet requirements"
        />
        <StatusInputField
          label="Verify Password"
          name="verifyPassword"
          type="password"
          status="error"
          errorMessage="Password does not match"
        />
        <button className="mt-4 w-full bg-black text-white rounded-lg py-3 hover:bg-gray-900 transition">
          Save
        </button>
      </div>

      {/* Right Section: Security Settings */}
      <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6">
        <h2 className="text-lg font-semibold">Security</h2>
         <div className="my-5">
           <DescToggle
                title="Multi-Factor Authentication"
                description="Add an extra layer of security to your account."
                name="mfa"
            />
        </div>
        <div className="my-5">
           <DescToggle
                title="Redact Resume"
                description="Share resume without personal info."
                name="redactedResume"
            />
        </div>
        <div>
            <SelectField
                label="Timeout Duration"
                name="workingHoursEnd"
                value={selected}
                onChange={(e) => setSelected(e.target.value)}
                options={timeoutOptions}
            />
        </div>
      </div>
    </div>
  );
}