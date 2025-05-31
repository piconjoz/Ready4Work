import StatusInputField from '../../../../components/StatusInputField';
import DescToggle from '../../../../components/DescToggle';
import { useState } from "react";
import SelectField from '../../../../components/SelectField';

export default function ApplicationInfo() {

    const [selected, setSelected] = useState("0000");
    
    const renumerationOptions = [
        { value: "0000", label: "Hourly" },
        { value: "0100", label: "Daily" },
        { value: "0200", label: "Monthly" },
        { value: "0300", label: "Session" },
    ];

    const skillsetOptions = [
        { value: "0000", label: "JavaScript" },
        { value: "0100", label: "Python" },
        { value: "0200", label: "Java" },
        { value: "0300", label: "C++" },
    ];

    return (
     <div className="grid grid-cols-1 lg:grid-cols-3 gap-6 md:gap-2 mt-12 mb-10">
       {/* Job Benefits (spans 1 col on md+) */}
       <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6 lg:col-span-1">
            <h2 className="text-lg font-semibold mb-4">Job Benefits</h2>
            <SelectField
                label="Renumeration Type"
                name="renumerationType"
                value={selected}
                onChange={(e) => setSelected(e.target.value)}
                options={renumerationOptions}
            />
            <StatusInputField
                label="Rate"
                name="rate"
                type="text"
            />
            <StatusInputField
            label="Other Benefits"
            name="otherBenefits"
            type="text"
            />
            <SelectField
                label="Skillsets"
                name="skillsets"
                value={selected}
                onChange={(e) => setSelected(e.target.value)}
                options={skillsetOptions}
            />
            <button className="mt-4 w-full bg-black text-white rounded-lg py-3 hover:bg-gray-900 transition">
            Save
            </button>
       </div>
 
       {/* Job Summary (spans 2 cols on md+) */}
       <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6 lg:col-span-2">
         <h2 className="text-lg font-semibold">Job Summary</h2> 
         <div className="grid grid-cols-1 lg:grid-cols-2 gap-1">
           <StatusInputField
             label="Job Duration"
             name="jobDuration"
             type="text"
             status="default"
             value="07/09/2025-01/10/2026"
             readOnly={false}
           />
           <StatusInputField
             label="Working Location"
             name="workingLocation"
             type="text"
             status="default"
             value="1 Upper Changi Road"
             readOnly={false}
           />
           <StatusInputField
             label="Working Hours"
             name="workingHours"
             type="text"
             status="default"
             value="0900-1800"
             readOnly={false}
           />
           <StatusInputField
             label="Job Scheme"
             name="jobScheme"
             type="text"
             status="default"
             value="SIT Student Work Scheme"
             readOnly={false}
           />
         </div>
         <div className="mt-4">

         </div>
       </div>
     </div>
  );
}