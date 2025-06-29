import { useState, useEffect } from "react";
import StatusInputField from "../../../../components/StatusInputField";

export default function RecruiterContactForm({
  onValidationChange,
  showValidationErrors,
  onDataChange,
}) {
  const [formData, setFormData] = useState({
    firstName: "Dave",
    lastName: "Johnson",
    salutation: "Mr",
    department: "Research Developement",
    fullLegalName: "Dave Timothy Johnson",
    officeCode: "65",
    officeNumber: "68363371",
    designation: "CFO",
    email: "Dave@pokka.com.sg",
    mobileCode: "65",
    mobileNumber: "98646153",
  });

  // All fields are required
  const requiredFields = [
    "firstName",
    "lastName",
    "salutation",
    "department",
    "fullLegalName",
    "officeCode",
    "officeNumber",
    "designation",
    "email",
    "mobileCode",
    "mobileNumber",
  ];

  useEffect(() => {
    const isValid = requiredFields.every(
      (field) => formData[field] && formData[field].trim() !== ""
    );
    onValidationChange("contact2", isValid); // Pass "contact2" as first argument
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
    <div className="grid grid-cols-1 xl:grid-cols-4 gap-x-2">
      <StatusInputField
        label="First Name"
        name="firstName"
        type="text"
        status={getFieldStatus("firstName")}
        errorMessage={
          getFieldStatus("firstName") === "error"
            ? "This field is required"
            : ""
        }
        value={formData.firstName}
        onChange={(e) => handleInputChange("firstName", e.target.value)}
      />
      <StatusInputField
        label="Last Name"
        name="lastName"
        type="text"
        status={getFieldStatus("lastName")}
        errorMessage={
          getFieldStatus("lastName") === "error" ? "This field is required" : ""
        }
        value={formData.lastName}
        onChange={(e) => handleInputChange("lastName", e.target.value)}
      />
      <StatusInputField
        label="Salutations"
        name="salutation"
        type="text"
        status={getFieldStatus("salutation")}
        errorMessage={
          getFieldStatus("salutation") === "error"
            ? "This field is required"
            : ""
        }
        value={formData.salutation}
        onChange={(e) => handleInputChange("salutation", e.target.value)}
      />
      <StatusInputField
        label="Department"
        name="department"
        type="text"
        status={getFieldStatus("department")}
        errorMessage={
          getFieldStatus("department") === "error"
            ? "This field is required"
            : ""
        }
        value={formData.department}
        onChange={(e) => handleInputChange("department", e.target.value)}
      />

      <StatusInputField
        className="xl:col-span-2"
        label="Full Legal Name"
        name="fullLegalName"
        type="text"
        status={getFieldStatus("fullLegalName")}
        errorMessage={
          getFieldStatus("fullLegalName") === "error"
            ? "This field is required"
            : ""
        }
        value={formData.fullLegalName}
        onChange={(e) => handleInputChange("fullLegalName", e.target.value)}
      />
      <StatusInputField
        label="Office Contact Country Code"
        name="officeCode"
        type="text"
        status={getFieldStatus("officeCode")}
        errorMessage={
          getFieldStatus("officeCode") === "error"
            ? "This field is required"
            : ""
        }
        value={formData.officeCode}
        onChange={(e) => handleInputChange("officeCode", e.target.value)}
      />
      <StatusInputField
        label="Office Contact Number"
        name="officeNumber"
        type="text"
        status={getFieldStatus("officeNumber")}
        errorMessage={
          getFieldStatus("officeNumber") === "error"
            ? "This field is required"
            : ""
        }
        value={formData.officeNumber}
        onChange={(e) => handleInputChange("officeNumber", e.target.value)}
      />

      <StatusInputField
        label="Designation"
        name="designation"
        type="text"
        status={getFieldStatus("designation")}
        errorMessage={
          getFieldStatus("designation") === "error"
            ? "This field is required"
            : ""
        }
        value={formData.designation}
        onChange={(e) => handleInputChange("designation", e.target.value)}
      />
      <StatusInputField
        label="Email"
        name="email"
        type="email"
        status={getFieldStatus("email")}
        errorMessage={
          getFieldStatus("email") === "error" ? "This field is required" : ""
        }
        value={formData.email}
        onChange={(e) => handleInputChange("email", e.target.value)}
      />
      <StatusInputField
        label="Mobile Contact Country Code"
        name="mobileCode"
        type="text"
        status={getFieldStatus("mobileCode")}
        errorMessage={
          getFieldStatus("mobileCode") === "error"
            ? "This field is required"
            : ""
        }
        value={formData.mobileCode}
        onChange={(e) => handleInputChange("mobileCode", e.target.value)}
      />
      <StatusInputField
        label="Mobile Contact Number"
        name="mobileNumber"
        type="text"
        status={getFieldStatus("mobileNumber")}
        errorMessage={
          getFieldStatus("mobileNumber") === "error"
            ? "This field is required"
            : ""
        }
        value={formData.mobileNumber}
        onChange={(e) => handleInputChange("mobileNumber", e.target.value)}
      />
    </div>
  );
}
