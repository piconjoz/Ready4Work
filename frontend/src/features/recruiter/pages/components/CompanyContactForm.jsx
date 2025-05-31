import StatusInputField from "../../../../components/StatusInputField";

export default function CompanyInfoForm() {
  return (
    <div className="bg-[#F8F9FD]">
      <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-4 gap-2 mt-6">
        <StatusInputField label="Street" name="street" type="text" status="default" defaultValue="1 Punggol Coast Road" readOnly className="xl:col-span-2" />
        <StatusInputField label="Unit Number" name="unit" type="text" status="default" defaultValue="01#135" readOnly />
        <StatusInputField label="Block" name="block" type="text" status="default" defaultValue="5A" readOnly />
        <StatusInputField label="City" name="city" type="text" status="default" defaultValue="Singapore" readOnly />
        <StatusInputField label="State" name="state" type="text" status="default" defaultValue="Singapore" readOnly />
        <StatusInputField label="Floor" name="floor" type="text" status="default" defaultValue="25" readOnly />
        <StatusInputField label="Postal Code" name="postal" type="text" status="default" defaultValue="736153" readOnly />
        <StatusInputField label="Zone Location" name="zone" type="text" status="default" defaultValue="North" readOnly />
        <StatusInputField label="Country Code" name="countryCode" type="text" status="default" defaultValue="65" readOnly />
        <StatusInputField label="Area Code" name="areaCode" type="text" status="default" defaultValue="-" readOnly />
        <StatusInputField label="Company Main Contact Number" name="contact" type="text" status="default" defaultValue="68363371" readOnly />
      </div>
    </div>
  );
}