import StatusInputField from '../../../../components/StatusInputField';
import DescToggle from '../../../../components/DescToggle';
import { useState, useRef, useEffect, useCallback } from "react";
import SelectField from '../../../../components/SelectField';
import Accordian from '../../../../components/Accordian';
import { uploadResume } from '../../../../services/resumeApi';
import { getPreference, savePreference } from '../../../../services/preferenceAPI';
import { getCurrentUser } from '../../../../services/authAPI';

export default function Resume() {
  // File upload state
  const [selectedFile, setSelectedFile] = useState(null);
  const [progress, setProgress] = useState(0);
  const [uploading, setUploading] = useState(false);
  const fileInputRef = useRef(null);
  const lastFileNameRef = useRef("");

  // Preference state
  const [remuneration, setRemuneration] = useState("hourly");
  const [workHoursStart, setWorkHoursStart] = useState("0900");
  const [workHoursEnd, setWorkHoursEnd] = useState("1700");
  const [redactedResume, setRedactedResume] = useState(false);
  const [hideInfoUntilOffered, setHideInfoUntilOffered] = useState(false);
  const [disableDownload, setDisableDownload] = useState(false);

  const user = getCurrentUser();
  const applicantId = user?.applicantId;

  console.log("Current user:", user);
  console.log("Applicant ID:", applicantId);

  useEffect(() => {
    if (user?.applicantId) {
      localStorage.setItem("applicantId", user.applicantId);
    }
  }, [user]);

  // Converts SelectField or event to primitive string
  const getPrimitive = useCallback((v, fallback = "") => {
    if (typeof v === "string") return v;
    if (v && v.value) return v.value;
    if (v?.target?.value) return v.target.value;
    return fallback;
  }, []);

  // Centralized function to save all preferences
  const saveAllPreferences = useCallback((updates = {}) => {
    if (!applicantId) {
      console.warn("saveAllPreferences: No applicantId available, skipping save.");
      return;
    }

    const dto = {
      applicantId,
      redactedResume,
      hideInfoUntilOffered,
      disableDownload,
      remunerationType: remuneration,
      workingHoursStart: workHoursStart,
      workingHoursEnd: workHoursEnd,
      ...updates // Overwrite with any specific updates passed in
    };

    console.log("Saving preferences DTO:", dto); // Log DTO being sent

    savePreference(applicantId, dto)
      .then(() => {
        console.log("Preferences saved successfully!");
      })
      .catch(err => {
        console.error("savePreference error:", err); // Log save errors
        // You might want to revert state here if the save failed
      });
  }, [
    applicantId,
    redactedResume,
    hideInfoUntilOffered,
    disableDownload,
    remuneration,
    workHoursStart,
    workHoursEnd
  ]);

  // On mount: fetch or create defaults
  useEffect(() => {
    if (!applicantId) {
      console.warn("useEffect: No applicantId available, skipping preference fetch.");
      return;
    }

    const fetchOrCreatePreferences = async () => {
      console.log(`Attempting to fetch preferences for applicantId: ${applicantId}`);
      try {
        const dto = await getPreference(applicantId);
        console.log("Fetched preferences DTO:", dto); // Log fetched data
        setRedactedResume(!!dto.redactedResume); // Ensure boolean
        setHideInfoUntilOffered(!!dto.hideInfoUntilOffered); // Ensure boolean
        setDisableDownload(!!dto.disableDownload); // Ensure boolean
        setRemuneration(dto.remunerationType);
        setWorkHoursStart(dto.workingHoursStart);
        setWorkHoursEnd(dto.workingHoursEnd);
        console.log("Preferences state updated from fetched data.");
      } catch (err) {
        if (err.response?.status === 404) {
          console.log("Preferences not found (404), creating defaults...");
          const defaults = {
            applicantId,
            redactedResume: false,
            hideInfoUntilOffered: false,
            disableDownload: false,
            remunerationType: "hourly",
            workingHoursStart: "0900",
            workingHoursEnd: "1700",
          };
          try {
            const dto = await savePreference(applicantId, defaults);
            console.log("Saved default preferences DTO:", dto); // Log saved default data
            setRedactedResume(!!dto.redactedResume);
            setHideInfoUntilOffered(!!dto.hideInfoUntilOffered);
            setDisableDownload(!!dto.disableDownload);
            setRemuneration(dto.remunerationType);
            setWorkHoursStart(dto.workingHoursStart);
            setWorkHoursEnd(dto.workingHoursEnd);
            console.log("Preferences state updated from default saved data.");
          } catch (saveErr) {
            console.error("Failed to save default preferences:", saveErr);
          }
        } else {
          console.error("Failed to fetch preferences:", err); // Log other fetch errors
        }
      }
    };

    fetchOrCreatePreferences();
  }, [applicantId]);


  // Upload helpers
  const handleUpload = async (file, id) => {
    if (!file) return;
    if (file.name !== lastFileNameRef.current) {
      setProgress(0);
      setTimeout(() => setProgress(100), 50);
      lastFileNameRef.current = file.name;
    }
    setUploading(true);
    try {
      const data = await uploadResume(file, id);
      console.log("Upload response:", data);
    } catch (err) {
      console.error("Upload error:", err);
    } finally {
      setUploading(false);
    }
  };

  const handleClick = () => {
    if (fileInputRef.current) {
      fileInputRef.current.value = null;
    }
    fileInputRef.current.click();
  };

  const handleFileChange = e => {
    const f = e.target.files[0];
    setSelectedFile(f);
    if (f) {
      handleUpload(f, applicantId);
    }
  };

  const handleDrop = e => {
    e.preventDefault();
    const f = e.dataTransfer.files[0];
    setSelectedFile(f);
    if (f) {
      handleUpload(f, applicantId);
    }
  };

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
        {/* Top Grid Section: Upload, Settings, Preference */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 md:gap-2">
          {/* Left Section: Resume Upload */}
          <div
            className="border border-dashed border-[#D3D3D3] bg-[#F5F5F5] rounded-2xl p-6 cursor-pointer"
            onClick={handleClick}
            onDragOver={e => e.preventDefault()}
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
                onClick={(e) => {
                    e.stopPropagation();
                    handleUpload(selectedFile, applicantId);
                }}
              >
                {uploading ? "Uploading…" : selectedFile ? "Replace Resume" : "Upload Resume"}
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
                onChange={val => {
                  setRedactedResume(val);
                  saveAllPreferences({ redactedResume: val });
                }}
              />
            </div>
            <div className="my-5">
              <DescToggle
                title="Hide My Info Until Offered"
                description="Details visible only after offer."
                name="hideInfoUntilOffered"
                checked={hideInfoUntilOffered}
                onChange={val => {
                  setHideInfoUntilOffered(val);
                  saveAllPreferences({ hideInfoUntilOffered: val });
                }}
              />
            </div>
            <div className="my-5">
              <DescToggle
                title="Disable Download"
                description="Resume viewable, not downloadable."
                name="disableDownload"
                checked={disableDownload}
                onChange={val => {
                  setDisableDownload(val);
                  saveAllPreferences({ disableDownload: val });
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
                onChange={val => {
                  const primitive = getPrimitive(val);
                  setRemuneration(primitive);
                  saveAllPreferences({ remunerationType: primitive });
                }}
                options={[
                  { value: "hourly", label: "Hourly" },
                  { value: "monthly", label: "Monthly" },
                  { value: "fixed", label: "Fixed" }
                ]}
              />
            </div>
            <div className="mb-4">
              <SelectField
                label="Working Hours Start"
                name="workingHoursStart"
                value={workHoursStart}
                onChange={val => {
                  const primitive = getPrimitive(val);
                  setWorkHoursStart(primitive);
                  saveAllPreferences({ workingHoursStart: primitive });
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
                onChange={val => {
                  const primitive = getPrimitive(val);
                  setWorkHoursEnd(primitive);
                  saveAllPreferences({ workingHoursEnd: primitive });
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

        {/* Bottom Section: Summarized Info */}
        <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6">
          <h2 className="text-lg font-semibold mb-4">Summarized Info</h2>
          <div className="my-4">
            <Accordian
              items={[
                {
                  question: "Professional Summary",
                  answer: "Detail-oriented Food Technology graduate with a passion for product development and hands-on experience in sensory testing. Seeking a role to apply my skills in real-world food innovation."
                },
                {
                  question: "Education",
                  answer: "BSc in Food Technology, Singapore Institute of Technology, 2024"
                },
                {
                  question: "Work Experience / Internships",
                  answer: "R&D Intern at F&N Foods, 3 months experience with sensory evaluation and prototype testing."
                },
                {
                  question: "Skills",
                  answer: "Sensory Testing, Product Development, Technical Documentation"
                },
                {
                  question: "Certifications / Training",
                  answer: "HACCP Certification, Food Safety Training Level 1"
                },
                {
                  question: "Leadership & Activities",
                  answer: "President, Food Science Club; Volunteer Tutor for underprivileged students."
                },
                {
                  question: "Phone Number & Email*",
                  answer: "Phone: 98765432 | Email: 2213113@sit.edu.sg"
                }
              ]}
            />
          </div>
        </div>
      </div>
    </>
  );
}