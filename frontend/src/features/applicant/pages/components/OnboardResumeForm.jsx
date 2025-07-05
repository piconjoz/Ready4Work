import StatusInputField from '../../../../components/StatusInputField';
import DescToggle from '../../../../components/DescToggle';
import { useState, useRef, useEffect } from "react";
import SelectField from '../../../../components/SelectField';
import { uploadResume } from '../../../../services/resumeApi';
import { getPreference, savePreference } from '../../../../services/preferenceAPI';
import { getCurrentUser } from '../../../../services/authAPI';

// Converts whatever SelectField gives us into a plain string.
// If we can't extract one, we fall back to 'fallback'.
const getPrimitive = (v, fallback = "") => {
  if (typeof v === "string") return v; // already a string
  if (v && typeof v === "object") {
    if (typeof v.value === "string") return v.value; // option object
    if (v.target && typeof v.target.value === "string") return v.target.value; // native <select> event
  }
  return fallback; // never undefined
};

export default function OnboardResumeForm() {

const [remuneration,   setRemuneration]   = useState("hourly");
const [workHoursStart, setWorkHoursStart] = useState("0900");
const [workHoursEnd,   setWorkHoursEnd]   = useState("1700");
const [selectedFile, setSelectedFile] = useState(null);
const [progress, setProgress] = useState(0);
const [uploading, setUploading] = useState(false);
const fileInputRef = useRef(null);
const lastFileNameRef = useRef("");

const handleClick = () => {
  // Reset the input so selecting the same file still triggers onChange
  fileInputRef.current.value = null;
  fileInputRef.current.click();
};

const handleFileChange = async (e) => {
  const file = e.target.files[0];
  if (file) {
    console.log("File selected via input:", file);
    setSelectedFile(file);
    await handleUpload(file, applicantId);  // immediately upload after selection
  }
};

const handleDragOver = (e) => {
  e.preventDefault();
  e.stopPropagation();
};

const handleDrop = async (e) => {
  e.preventDefault();
  e.stopPropagation();
  const file = e.dataTransfer.files[0];
  if (file) {
    console.log("File dropped:", file);
    // Clear input to allow reupload of same file
    if (fileInputRef.current) fileInputRef.current.value = null;
    setSelectedFile(file);
    await handleUpload(file, applicantId);  // immediately upload after drop
  }
};

const handleUpload = async (fileToUpload, applicantId) => {
  if (!fileToUpload) return;
  // Animate only when the file name changes
  if (fileToUpload.name !== lastFileNameRef.current) {
    setProgress(0);
    setTimeout(() => setProgress(100), 50);
    lastFileNameRef.current = fileToUpload.name;
  }
  console.log("Uploading resume via resumeApi:", fileToUpload);
  setUploading(true);
  try {
    const data = await uploadResume(fileToUpload, applicantId);
    console.log("Upload response data:", data);
    // Optionally refresh list or update UI state
  } catch (err) {
    console.error("Upload error:", err);
  } finally {
    setUploading(false);
  }
};

const timeoutOptions = [
    { value: "0000", label: "5 Min" },
    { value: "0100", label: "10 Min" },
    { value: "0200", label: "15 Min" },
    { value: "0300", label: "1 Day" },
    { value: "2300", label: "1 Month" },
];

const user = getCurrentUser();
const applicantId = user?.applicantId;

const [redactedResume, setRedactedResume] = useState(false);
const [hideInfoUntilOffered, setHideInfoUntilOffered] = useState(false);
const [disableDownload, setDisableDownload] = useState(false);

useEffect(() => {
  if (!applicantId) return;

  const defaults = {
    applicantId,
    redactedResume: false,
    hideInfoUntilOffered: false,
    disableDownload: false,
    remunerationType: remuneration,
    workingHoursStart: workHoursStart,
    workingHoursEnd: workHoursEnd,
  };

  // First try fetching existing preferences
  getPreference(applicantId)
    .then(dto => {
      console.log("getPreference → loaded:", dto);
      setRedactedResume(dto.redactedResume);
      setHideInfoUntilOffered(dto.hideInfoUntilOffered);
      setDisableDownload(dto.disableDownload);
      setRemuneration(dto.remunerationType);
      setWorkHoursStart(dto.workingHoursStart);
      setWorkHoursEnd(dto.workingHoursEnd);
    })
    .catch(err => {
      // If not found, create default preferences
      if (err.response?.status === 404) {
        console.log("No existing preferences, creating defaults:", defaults);
        savePreference(applicantId, defaults)
          .then(dto => {
            console.log("savePreference → created defaults:", dto);
            setRedactedResume(dto.redactedResume);
            setHideInfoUntilOffered(dto.hideInfoUntilOffered);
            setDisableDownload(dto.disableDownload);
            setRemuneration(dto.remunerationType);
            setWorkHoursStart(dto.workingHoursStart);
            setWorkHoursEnd(dto.workingHoursEnd);
          })
          .catch(e => console.error("savePreference (defaults) → error:", e));
      } else {
        console.error("getPreference → error:", err);
      }
    });
}, [applicantId]);

    return (
  <>
    <input
      type="file"
      accept=".pdf,image/png"
      ref={fileInputRef}
      style={{ display: "none" }}
      onChange={handleFileChange}
    />
  <div className="space-y-6 mt-6">
    {/* Top Grid Section */}
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 md:gap-2">
      {/* Left Section: Resume Upload */}
      <div
        className="border border-dashed border-[#D3D3D3] bg-[#F5F5F5] rounded-2xl p-6 cursor-pointer"
        onClick={handleClick}
        onDragOver={handleDragOver}
        onDrop={handleDrop}
      >
        <h2 className="text-lg font-semibold mb-4">Resume</h2>
        <div className="bg-[#F5F5F5] rounded-xl py-20 text-sm text-black flex flex-col items-center justify-center">
          <div className="w-full h-1 bg-gray-300 rounded-full mb-4">
            <div
              className="h-1 bg-blue-600 rounded-full transition-all duration-500 ease-in-out"
              style={{ width: `${progress}%` }}
            ></div>
          </div>
          <div className="mb-2 font-medium">
            {selectedFile ? selectedFile.name : "Click or drag here to upload resume"}
          </div>
          
          <button
            className="bg-[#E6E6E6] rounded-2xl px-4 py-2 text-sm"
            onClick={async () => {
              await handleUpload(selectedFile, applicantId);
            }}
          >
            {selectedFile ? "Replace Resume" : "Upload Resume"}
          </button>
          
        </div>
      </div>

      {/* Middle Section: Resume Settings */}
      <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6">
        <h2 className="text-lg font-semibold mb-4">Resume Settings</h2>
        <div className="my-5">
          <DescToggle
            title="Redacted Resume"
            description="Share resume without personal info."
            name="redactedResume"
            checked={redactedResume}
            onChange={(val) => {
              setRedactedResume(val);
              if (applicantId) {
                const dto = {
                  applicantId,
                  redactedResume: val, // Use the new value
                  hideInfoUntilOffered: hideInfoUntilOffered,
                  disableDownload: disableDownload,
                  remunerationType: remuneration,
                  workingHoursStart: workHoursStart,
                  workingHoursEnd: workHoursEnd,
                };
                console.log("savePreference → Redacted Resume change payload:", dto); // Added log
                savePreference(applicantId, dto)
                  .then(resp => console.log("savePreference → response:", resp)) // Added
                  .catch(err => console.error("savePreference → error:", err));   // Added
              }
            }}
          />        </div>
        <div className="my-5">
          <DescToggle
            title="Hide My Info Until Offered"
            description="Details visible only after offer."
            name="hideInfoUntilOffered"
            checked={hideInfoUntilOffered}
            onChange={(val) => {
              setHideInfoUntilOffered(val);
              if (applicantId) {
                const dto = {
                  applicantId,
                  redactedResume: redactedResume,
                  hideInfoUntilOffered: val, // Use the new value
                  disableDownload: disableDownload,
                  remunerationType: remuneration,
                  workingHoursStart: workHoursStart,
                  workingHoursEnd: workHoursEnd,
                };
                console.log("savePreference → Hide My Info Until Offered change payload:", dto); // Added log
                savePreference(applicantId, dto)
                  .then(resp => console.log("savePreference → response:", resp)) // Added
                  .catch(err => console.error("savePreference → error:", err));   // Added
              }
            }}
          />
        </div>
        <div className="my-5">
         <DescToggle
            title="Disable Download"
            description="Resume viewable, not downloadable."
            name="disableDownload"
            checked={disableDownload}
            onChange={(val) => {
              setDisableDownload(val);
              if (applicantId) {
                const dto = {
                  applicantId,
                  redactedResume: redactedResume,
                  hideInfoUntilOffered: hideInfoUntilOffered,
                  disableDownload: val, // Use the new value
                  remunerationType: remuneration,
                  workingHoursStart: workHoursStart,
                  workingHoursEnd: workHoursEnd,
                };
                console.log("savePreference → Disable Download change payload:", dto); // Added log
                savePreference(applicantId, dto)
                  .then(resp => console.log("savePreference → response:", resp)) // Added
                  .catch(err => console.error("savePreference → error:", err));   // Added
              }
            }}
          />
        </div>
      </div>

      {/* Right Section: Preferences */}
      <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6">
        <h2 className="text-lg font-semibold mb-4">Preference</h2>
        <div className="mb-4">
          <SelectField
            label="Remuneration Type"
            name="remuneration"
            value={remuneration}
            onChange={(val) => {
              const primitive = getPrimitive(val, remuneration);
              setRemuneration(primitive);

              if (!applicantId) return;

              const dto = {
                applicantId,
                redactedResume,
                hideInfoUntilOffered,
                disableDownload,
                remunerationType: primitive,
                workingHoursStart: workHoursStart,
                workingHoursEnd:   workHoursEnd,
              };

              console.log("savePreference → Remuneration change payload:", dto);

              savePreference(applicantId, dto)
                .then(resp => console.log("savePreference → response:", resp))
                .catch(err  => console.error("savePreference → error:", err));
            }}
            options={[
              { value: "hourly",  label: "Hourly"  },
              { value: "monthly", label: "Monthly" },
              { value: "fixed",   label: "Fixed"   }
            ]}
          />
        </div>
        <div className="mb-4">
          <SelectField
            label="Working Hours Start"
            name="workingHoursStart"
            value={workHoursStart}
            onChange={(val) => {
              const primitive = getPrimitive(val, workHoursStart);
              setWorkHoursStart(primitive);
              if (applicantId) {
                const dto = { // Define dto for logging
                  applicantId,
                  redactedResume,
                  hideInfoUntilOffered,
                  disableDownload,
                  remunerationType: remuneration,
                  workingHoursStart: primitive, // Use the new value
                  workingHoursEnd:   workHoursEnd,
                };
                console.log("savePreference → Working Hours Start change payload:", dto); // Added log
                savePreference(applicantId, dto)
                  .then(resp => console.log("savePreference → response:", resp)) // Added
                  .catch(err => console.error("savePreference → error:", err));   // Added
              }
            }}
            options={[
              { value: "0800", label: "0800" },
              { value: "0900", label: "0900" },
              { value: "1000", label: "1000" }
            ]}
          />
        </div>
        <div className="mb-4">
          <SelectField
            label="Working Hours End"
            name="workingHoursEnd"
            value={workHoursEnd}
            onChange={(val) => {
              const primitive = getPrimitive(val, workHoursEnd);
              setWorkHoursEnd(primitive);
              if (applicantId) {
                const dto = { // Define dto for logging
                  applicantId,
                  redactedResume,
                  hideInfoUntilOffered,
                  disableDownload,
                  remunerationType: remuneration,
                  workingHoursStart: workHoursStart,
                  workingHoursEnd:   primitive, // Use the new value
                };
                console.log("savePreference → Working Hours End change payload:", dto); // Added log
                savePreference(applicantId, dto)
                  .then(resp => console.log("savePreference → response:", resp)) // Added
                  .catch(err => console.error("savePreference → error:", err));   // Added
              }
            }}
            options={[
              { value: "1700", label: "1700" },
              { value: "1800", label: "1800" },
              { value: "0000", label: "0000" }
            ]}
          />
        </div>
      </div>
    </div>
  </div>
  </>
);
}