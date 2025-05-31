import AlertNote from "../../../../components/AlertNote";
import StatusInputField from "../../../../components/StatusInputField";

export default function SettingCompanyInfo() {

    return (
    <div>
        <div className="flex items-center gap-4">
            {/* Avatar */}
            <img
                src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQiZG8aaViLv7di4yE8HU162B8BNTsCMv7_7w&s"
                alt="User Avatar"
                className="w-14 h-14 rounded-full object-cover"
            />

            {/* Name and Email */}
            <div className="flex flex-col">
                <span className="text-base font-medium text-black">Pokka Singapore</span>
                <span className="text-sm text-gray-500">hr@pokka.com.sg</span>
            </div>
        </div>
        <div className="mt-10">
            <AlertNote
                title="Note"
                message="Please contact Registrar's Office if you wish to update the personal details"
                bgColor="#727272"
            />
        </div>

         <div className="grid grid-cols-1 xl:grid-cols-4 gap-2 mt-6">
             <StatusInputField label="Registered Company Name" name="registeredName" type="text" status="default" defaultValue="Pokka Singapore" readOnly />
             <StatusInputField label="Preferred Company Name" name="preferredName" type="text" status="default" defaultValue="Pokka SG" readOnly />
             <StatusInputField label="Country of Business Registration" name="country" type="text" status="default" defaultValue="Singapore" readOnly />
             <StatusInputField label="UEN" name="uen" type="text" status="default" defaultValue="2020" readOnly />
             <StatusInputField label="Number of Employees" name="employees" type="text" status="default" defaultValue="10" readOnly />
             <StatusInputField label="Industry Cluster" name="industry" type="text" status="default" defaultValue="Food Manufacturing" readOnly />
             <StatusInputField  className="xl:col-span-2" label="Entity Type" name="entityType" type="text" status="default" defaultValue="Non-Governmental Organisations (NGOs), Non-Profit Organisations (NPO), Social Service Agencies (SSA)" readOnly />
             <StatusInputField label="Authorised Training Organisation" name="ato" type="text" status="default" defaultValue="Yes" readOnly />
             <StatusInputField label="Company Website" name="website" type="text" status="default" defaultValue="www.pokka.com.sg" readOnly />
             <StatusInputField  className="xl:col-span-2" label="Employment Type Interested In" name="employmentType" type="text" status="default" defaultValue="IWSP – Integrated Work Study Programme / TITO – Term-In Term-Out / TITO-IWL – Integrated Workplace Learning" readOnly />
            
             <StatusInputField
              className="col-span-full"
               label="Company Profile"
               name="profile"
               type="textarea"
               status="default"
               defaultValue="Pokka Singapore is a leading beverage manufacturer and distributor known for its wide range of ready-to-drink coffee, teas, juices, and functional drinks. Established in 1977, Pokka is headquartered in Singapore and has built a strong presence across Asia and the Middle East. The company is committed to high-quality production, innovation, and health-conscious formulations, with many of its beverages brewed using authentic methods. As part of the Pokka Sapporo Food & Beverage group from Japan, Pokka Singapore combines Japanese quality with local taste preferences, making it a household name in the region’s beverage industry."
               readOnly
                 
             />
        </div>
    </div>
  );
}