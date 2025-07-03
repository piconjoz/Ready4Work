import AlertNote from "../../../../components/AlertNote";
import StatusInputField from "../../../../components/StatusInputField";
import { useState, useEffect } from "react";
import { getCurrentUser, getStudentProfile } from "../../../../services/authAPI";
// key for caching profile in localStorage
const STORAGE_KEY = "studentProfile";

export default function Profile() {
  // load cached profile from storage
  const stored = localStorage.getItem(STORAGE_KEY);
  const initialProfile = stored ? JSON.parse(stored) : null;
  const [profile, setProfile] = useState(initialProfile);
  const [error, setError] = useState(null);
  const user = getCurrentUser();

  useEffect(() => {
    console.log("Profile.useEffect: fetching profile for", user?.email);
    if (user?.email) {
      getStudentProfile(user.email)
        .then(dto => {
          console.log("getStudentProfile → success:", dto);
          setProfile(dto);
          localStorage.setItem(STORAGE_KEY, JSON.stringify(dto));
        })
        .catch(err => {
          console.error("getStudentProfile → error:", err);
          setError(err);
        });
    }
  }, [user?.email]);

  if (error) {
    return <p className="text-red-600">Error loading profile: {error.message || error.toString()}</p>;
  }

  if (!profile) {
    return <p>Loading profile…</p>;
  }

  return (
    <div>
      <div className="flex items-center gap-4">
        {/* Avatar */}
        <img
          src="https://encrypted-tbn1.gstatic.com/licensed-image?q=tbn:ANd9GcRHXt1lEDp5xR62TWzq6FzXcZMTNuklFwmDQLQPpI8WEC1yOXp1pglw1v7dUBw83rjPiHJ_QTHvVoFGNog"
          alt="User Avatar"
          className="w-14 h-14 rounded-full object-cover"
        />

        {/* Name and Email */}
        <div className="flex flex-col">
          <span className="text-base font-medium text-black">{profile.fullName}</span>
          <span className="text-sm text-gray-500">{profile.Email}</span>
        </div>
      </div>
      <div className="mt-10">
        <AlertNote
          title="Note"
          message="Please contact Registrar's Office if you wish to update the personal details"
          bgColor="#727272"
        />
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-2 mt-6">
        <StatusInputField
          label="NRIC/FIN"
          name="nric"
          type="text"
          status="default"
          errorMessage=""
          value={profile.nricFin}
          readOnly
        />
        <StatusInputField
          label="Student ID"
          name="studentId"
          type="text"
          status="default"
          errorMessage=""
          value={profile.studentId}
          readOnly
        />
        <StatusInputField
          label="Nationality"
          name="nationality"
          type="text"
          status="default"
          errorMessage=""
          value={profile.nationality}
          readOnly
        />
        <StatusInputField
          label="Admit Year"
          name="admitYear"
          type="text"
          status="default"
          errorMessage=""
          value={profile.admitYear}
          readOnly
        />
        <StatusInputField
          label="Primary Contact Number"
          name="contact"
          type="text"
          status="default"
          errorMessage=""
          value={profile.primaryContactNumber}
          readOnly
        />
        <StatusInputField
          label="Gender"
          name="gender"
          type="text"
          status="default"
          errorMessage=""
          value={profile.gender}
          readOnly
        />
        <StatusInputField
          label="Degree Programme"
          name="programme"
          type="text"
          status="default"
          errorMessage=""
          value={profile.degreeProgramme}
          readOnly
          className="col-span-full"
        />
      </div>
    </div>
  );
}