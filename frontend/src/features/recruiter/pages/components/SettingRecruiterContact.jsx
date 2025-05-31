import AlertNote from "../../../../components/AlertNote";
import StatusInputField from "../../../../components/StatusInputField";

export default function SettingRecruiterContact() {

    return (
    <div>
        <div className="">
            <AlertNote
                title="Note"
                message="Please contact Registrar's Office if you wish to update the personal details"
                bgColor="#727272"
            />
        </div>

       <div className="grid grid-cols-1 xl:grid-cols-4 gap-2">
         <StatusInputField label="First Name" name="firstName" type="text" status="default" defaultValue="Dave" readOnly />
         <StatusInputField label="Last Name" name="lastName" type="text" status="default" defaultValue="Johnson" readOnly />
         <StatusInputField label="Salutations" name="salutation" type="text" status="default" defaultValue="Mr" readOnly />
         <StatusInputField label="Department" name="department" type="text" status="default" defaultValue="Research Developement" readOnly />
 
         <StatusInputField className="xl:col-span-2" label="Full Legal Name" name="fullLegalName" type="text" status="default" defaultValue="Dave Timothy Johnson" readOnly />
         <StatusInputField label="Office Contact Country Code" name="officeCode" type="text" status="default" defaultValue="65" readOnly />
         <StatusInputField label="Office Contact Number" name="officeNumber" type="text" status="default" defaultValue="68363371" readOnly />
 
         <StatusInputField label="Designation" name="designation" type="text" status="default" defaultValue="CFO" readOnly />
         <StatusInputField label="Email" name="email" type="text" status="default" defaultValue="Dave@pokka.com.sg" readOnly />
         <StatusInputField label="Mobile Contact Country Code" name="mobileCode" type="text" status="default" defaultValue="65" readOnly />
         <StatusInputField label="Mobile Contact Number" name="mobileNumber" type="text" status="default" defaultValue="98646153" readOnly />
       </div>
    </div>
  );
}