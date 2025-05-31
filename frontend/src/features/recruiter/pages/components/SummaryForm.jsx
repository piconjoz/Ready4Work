import StatusInputField from "../../../../components/StatusInputField";

export default function SummaryForm() {
  return (
      <div className="grid grid-cols-1 xl:grid-cols-4 gap-2">
        <StatusInputField label="Designated Account Email" name="accountEmail" type="email" status="default" defaultValue="account@example.com" readOnly />
        <StatusInputField label="Password" name="password" type="password" status="default" defaultValue="********" readOnly />
        <StatusInputField label="Account Type" name="accountType" type="text" status="default" defaultValue="Recruiter" readOnly />
        <StatusInputField label="Registration Date" name="registrationDate" type="text" status="default" defaultValue="2025-06-01" readOnly />
      </div>
  );
}