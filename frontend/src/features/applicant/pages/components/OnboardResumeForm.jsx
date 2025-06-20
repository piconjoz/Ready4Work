import StatusInputField from '../../../../components/StatusInputField';
import DescToggle from '../../../../components/DescToggle';
import { useState } from "react";
import SelectField from '../../../../components/SelectField';

export default function OnboardResumeForm() {


const [selected, setSelected] = useState("0000");

const timeoutOptions = [
    { value: "0000", label: "5 Min" },
    { value: "0100", label: "10 Min" },
    { value: "0200", label: "15 Min" },
    { value: "0300", label: "1 Day" },
    { value: "2300", label: "1 Month" },
];


    return (
  <div className="space-y-6 mt-6">
    {/* Top Grid Section */}
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 md:gap-2">
      {/* Left Section: Resume Upload */}
      <div className="border border-dashed border-[#D3D3D3] bg-[#F5F5F5] rounded-2xl p-6">
        <h2 className="text-lg font-semibold mb-4">Resume</h2>
        <div className="bg-[#F5F5F5] rounded-xl py-20 text-sm text-black flex flex-col items-center justify-center">
          <div className="w-full h-1 bg-gray-300 rounded-full mb-4">
            <div
              className="h-1 bg-blue-600 rounded-full transition-all duration-500 ease-in-out"
              style={{ width: '60%' }}
            ></div>
          </div>
          <div className="mb-2 font-medium">Resume_2023.pdf</div>
          <button className="bg-[#E6E6E6] rounded-2xl px-4 py-2 text-sm">Update Resume</button>
        </div>
      </div>

      {/* Middle Section: Resume Settings */}
      <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6">
        <h2 className="text-lg font-semibold mb-4">Resume Settings</h2>
        <div className="my-5">
          <DescToggle title="Redacted Resume" description="Share resume without personal info." name="redactedResume" />
        </div>
        <div className="my-5">
          <DescToggle title="Hide My Info Until Offered" description="Details visible only after offer." name="hideInfo" />
        </div>
        <div className="my-5">
          <DescToggle title="Disable Download" description="Resume viewable, not downloadable." name="disableDownload" />
        </div>
      </div>

      {/* Right Section: Preferences */}
      <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6">
        <h2 className="text-lg font-semibold mb-4">Preference</h2>
        <div className="mb-4">
          <SelectField label="Remuneration Type" name="remuneration" value="hourly" onChange={() => {}} options={[
            { value: "hourly", label: "Hourly" },
            { value: "monthly", label: "Monthly" },
            { value: "fixed", label: "Fixed" }
          ]} />
        </div>
        <div className="mb-4">
          <SelectField label="Working Hours Start" name="workingHoursStart" value="0900" onChange={() => {}} options={[
            { value: "0800", label: "0800" },
            { value: "0900", label: "0900" },
            { value: "1000", label: "1000" }
          ]} />
        </div>
        <div className="mb-4">
          <SelectField label="Working Hours End" name="workingHoursEnd" value="0000" onChange={() => {}} options={[
            { value: "1700", label: "1700" },
            { value: "1800", label: "1800" },
            { value: "0000", label: "0000" }
          ]} />
        </div>
      </div>
    </div>
  </div>
);
}