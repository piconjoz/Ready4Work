import AlertNote from "../../../../components/AlertNote";
import StatusInputField from "../../../../components/StatusInputField";

export default function Profile() {

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
                <span className="text-base font-medium text-black">Lee Hsien Loong</span>
                <span className="text-sm text-gray-500">2941985@Sit.Singaporetech.edu.sg</span>
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
            defaultValue="S7193847A"
            readOnly
          />
          <StatusInputField
            label="Student ID"
            name="studentId"
            type="text"
            status="default"
            errorMessage=""
            defaultValue="2941985"
            readOnly
          />
          <StatusInputField
            label="Nationality"
            name="nationality"
            type="text"
            status="default"
            errorMessage=""
            defaultValue="Singapore"
            readOnly
          />
          <StatusInputField
            label="Admit Year"
            name="admitYear"
            type="text"
            status="default"
            errorMessage=""
            defaultValue="2020"
            readOnly
          />
          <StatusInputField
            label="Primary Contact Number"
            name="contact"
            type="text"
            status="default"
            errorMessage=""
            defaultValue="94304313"
            readOnly
          />
          <StatusInputField
            label="Gender"
            name="gender"
            type="text"
            status="default"
            errorMessage=""
            defaultValue="Male"
            readOnly
          />
          <StatusInputField
            label="Degree Programme"
            name="programme"
            type="text"
            status="default"
            errorMessage=""
            defaultValue="BA in User Experience and Game Design"
            readOnly
            className="col-span-full"
          />
        </div>
    </div>
  );
}