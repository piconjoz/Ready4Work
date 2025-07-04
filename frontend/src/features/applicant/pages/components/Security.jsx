import StatusInputField from '../../../../components/StatusInputField';
import DescToggle from '../../../../components/DescToggle';
import { validateCredentials, updatePassword } from '../../../../services/securityApi';
import { getCurrentUser } from '../../../../services/authAPI';
import { useState } from "react";
import SelectField from '../../../../components/SelectField';


export default function Security() {

  const user = getCurrentUser(); // holds { email, ... }
  const [currentPassword, setCurrentPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [verifyPassword, setVerifyPassword] = useState("");
  const [error, setError]     = useState("");
  const [success, setSuccess] = useState("");

  const [selected, setSelected] = useState("0000");

  const timeoutOptions = [
      { value: "0000", label: "5 Min" },
      { value: "0100", label: "10 Min" },
      { value: "0200", label: "15 Min" },
      { value: "0300", label: "1 Day" },
      { value: "2300", label: "1 Month" },
  ];

  const handleSave = async () => {
    setError("");
    setSuccess("");
    if (newPassword !== verifyPassword) {
      setError("New password and verify must match");
      return;
    }
    try {
      // validate current password
      await validateCredentials(user.email, currentPassword);
      // update to new password
      await updatePassword(newPassword);
      setSuccess("Password updated successfully");
      setCurrentPassword("");
      setNewPassword("");
      setVerifyPassword("");
    } catch (e) {
      setError(e.message || "An error occurred");
    }
  };

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3  gap-6 md:gap-2 mt-6">
      {/* Left Section: Change Password */}
      <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6">
        <h2 className="text-lg font-semibold mb-4">Change Password</h2>
        {error && <p className="text-red-600 mb-2">{error}</p>}
        {success && <p className="text-green-600 mb-2">{success}</p>}
        <StatusInputField
          label="Current Password"
          name="currentPassword"
          type="password"
          status={error.includes("Invalid credentials") ? "error" : "default"}
          errorMessage={error.includes("Invalid credentials") ? "Incorrect password" : ""}
          value={currentPassword}
          onChange={e => setCurrentPassword(e.target.value)}
        />
        <StatusInputField
          label="New Password"
          name="newPassword"
          type="password"
          status={error.includes("must match") ? "error" : "default"}
          errorMessage={error.includes("must match") ? "Passwords do not match" : ""}
          value={newPassword}
          onChange={e => setNewPassword(e.target.value)}
        />
        <StatusInputField
          label="Verify Password"
          name="verifyPassword"
          type="password"
          status={error.includes("must match") ? "error" : "default"}
          errorMessage={error.includes("must match") ? "Passwords do not match" : ""}
          value={verifyPassword}
          onChange={e => setVerifyPassword(e.target.value)}
        />
        <button
          onClick={handleSave}
          className="mt-4 w-full bg-black text-white rounded-lg py-3 hover:bg-gray-900 transition"
        >
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
        {/* <div className="my-5">
           <DescToggle
                title="Session Timeout"
                description="Automatically log out after a period of inactivity."
                name="sessionTimeout"
            />
             <SelectField
                label="Timeout Duration"
                name="workingHoursEnd"
                value={selected}
                onChange={(e) => setSelected(e.target.value)}
                options={timeoutOptions}
            />
        </div> */}
      </div>
    </div>
  );
}