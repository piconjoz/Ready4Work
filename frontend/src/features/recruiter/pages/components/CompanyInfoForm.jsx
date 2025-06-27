import { useState, useEffect } from "react";
import AlertNote from "../../../../components/AlertNote";
import StatusInputField from "../../../../components/StatusInputField";

export default function CompanyInfoForm({
  onValidationChange,
  showValidationErrors,
  onDataChange,
}) {
  const [formData, setFormData] = useState({
    registeredName: "Pokka Singapore",
    preferredName: "Pokka SG",
    country: "Singapore",
    uen: "2020",
    employees: "10",
    industry: "Food Manufacturing",
    entityType:
      "Non-Governmental Organisations (NGOs), Non-Profit Organisations (NPO), Social Service Agencies (SSA)",
    ato: "Yes",
    website: "www.pokka.com.sg",
    employmentType:
      "IWSP – Integrated Work Study Programme / TITO – Term-In Term-Out / TITO-IWL – Integrated Workplace Learning",
    profile:
      "Pokka Singapore is a leading beverage manufacturer and distributor known for its wide range of ready-to-drink coffee, teas, juices, and functional drinks. Established in 1977, Pokka is headquartered in Singapore and has built a strong presence across Asia and the Middle East. The company is committed to high-quality production, innovation, and health-conscious formulations, with many of its beverages brewed using authentic methods. As part of the Pokka Sapporo Food & Beverage group from Japan, Pokka Singapore combines Japanese quality with local taste preferences, making it a household name in the region's beverage industry.",
  });

  // All fields are required
  const requiredFields = [
    "registeredName",
    "preferredName",
    "country",
    "uen",
    "employees",
    "industry",
    "entityType",
    "ato",
    "website",
    "employmentType",
    "profile",
  ];

  // Check validation whenever form data changes
  useEffect(() => {
    const isValid = requiredFields.every(
      (field) => formData[field] && formData[field].trim() !== ""
    );
    onValidationChange("company", isValid); // Pass "company" as first argument
  }, [formData, onValidationChange]);

  // Pass data back to parent whenever formData changes
  useEffect(() => {
    if (onDataChange) {
      onDataChange(formData);
    }
  }, [formData, onDataChange]);

  const handleInputChange = (field, value) => {
    setFormData((prev) => ({
      ...prev,
      [field]: value,
    }));
  };

  // Check if field should show error
  const getFieldStatus = (fieldName) => {
    if (!showValidationErrors) return "default";
    const fieldValue = formData[fieldName];
    return !fieldValue || fieldValue.trim() === "" ? "error" : "default";
  };

  return (
    <div>
      <div className="grid grid-cols-1 xl:grid-cols-4 gap-x-2 mt-6">
        <StatusInputField
          label="Registered Company Name"
          name="registeredName"
          type="text"
          status={getFieldStatus("registeredName")}
          errorMessage={
            getFieldStatus("registeredName") === "error"
              ? "This field is required"
              : ""
          }
          value={formData.registeredName}
          onChange={(e) => handleInputChange("registeredName", e.target.value)}
        />
        <StatusInputField
          label="Preferred Company Name"
          name="preferredName"
          type="text"
          status={getFieldStatus("preferredName")}
          errorMessage={
            getFieldStatus("preferredName") === "error"
              ? "This field is required"
              : ""
          }
          value={formData.preferredName}
          onChange={(e) => handleInputChange("preferredName", e.target.value)}
        />
        <StatusInputField
          label="Country of Business Registration"
          name="country"
          type="text"
          status={getFieldStatus("country")}
          errorMessage={
            getFieldStatus("country") === "error"
              ? "This field is required"
              : ""
          }
          value={formData.country}
          onChange={(e) => handleInputChange("country", e.target.value)}
        />
        <StatusInputField
          label="UEN"
          name="uen"
          type="text"
          status={getFieldStatus("uen")}
          errorMessage={
            getFieldStatus("uen") === "error" ? "This field is required" : ""
          }
          value={formData.uen}
          onChange={(e) => handleInputChange("uen", e.target.value)}
        />
        <StatusInputField
          label="Number of Employees"
          name="employees"
          type="text"
          status={getFieldStatus("employees")}
          errorMessage={
            getFieldStatus("employees") === "error"
              ? "This field is required"
              : ""
          }
          value={formData.employees}
          onChange={(e) => handleInputChange("employees", e.target.value)}
        />
        <StatusInputField
          label="Industry Cluster"
          name="industry"
          type="text"
          status={getFieldStatus("industry")}
          errorMessage={
            getFieldStatus("industry") === "error"
              ? "This field is required"
              : ""
          }
          value={formData.industry}
          onChange={(e) => handleInputChange("industry", e.target.value)}
        />
        <StatusInputField
          className="xl:col-span-2"
          label="Entity Type"
          name="entityType"
          type="text"
          status={getFieldStatus("entityType")}
          errorMessage={
            getFieldStatus("entityType") === "error"
              ? "This field is required"
              : ""
          }
          value={formData.entityType}
          onChange={(e) => handleInputChange("entityType", e.target.value)}
        />
        <StatusInputField
          label="Authorised Training Organisation"
          name="ato"
          type="text"
          status={getFieldStatus("ato")}
          errorMessage={
            getFieldStatus("ato") === "error" ? "This field is required" : ""
          }
          value={formData.ato}
          onChange={(e) => handleInputChange("ato", e.target.value)}
        />
        <StatusInputField
          label="Company Website"
          name="website"
          type="text"
          status={getFieldStatus("website")}
          errorMessage={
            getFieldStatus("website") === "error"
              ? "This field is required"
              : ""
          }
          value={formData.website}
          onChange={(e) => handleInputChange("website", e.target.value)}
        />
        <StatusInputField
          className="xl:col-span-2"
          label="Employment Type Interested In"
          name="employmentType"
          type="text"
          status={getFieldStatus("employmentType")}
          errorMessage={
            getFieldStatus("employmentType") === "error"
              ? "This field is required"
              : ""
          }
          value={formData.employmentType}
          onChange={(e) => handleInputChange("employmentType", e.target.value)}
        />

        <StatusInputField
          className="col-span-full"
          label="Company Profile"
          name="profile"
          type="textarea"
          status={getFieldStatus("profile")}
          errorMessage={
            getFieldStatus("profile") === "error"
              ? "This field is required"
              : ""
          }
          value={formData.profile}
          onChange={(e) => handleInputChange("profile", e.target.value)}
        />
      </div>
    </div>
  );
}
