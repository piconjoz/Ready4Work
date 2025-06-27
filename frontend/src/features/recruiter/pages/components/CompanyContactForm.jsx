import { useState, useEffect } from "react";
import StatusInputField from "../../../../components/StatusInputField";

export default function CompanyContactForm({
  onValidationChange,
  showValidationErrors,
  onDataChange,
}) {
  const [formData, setFormData] = useState({
    street: "1 Punggol Coast Road",
    unit: "01#135",
    block: "5A",
    city: "Singapore",
    state: "Singapore",
    floor: "25",
    postal: "736153",
    zone: "North",
    countryCode: "65",
    areaCode: "-",
    contact: "68363371",
  });

  // All fields are required
  const requiredFields = [
    "street",
    "unit",
    "block",
    "city",
    "state",
    "floor",
    "postal",
    "zone",
    "countryCode",
    "areaCode",
    "contact",
  ];

  useEffect(() => {
    const isValid = requiredFields.every(
      (field) => formData[field] && formData[field].trim() !== ""
    );
    onValidationChange("contact1", isValid); // Pass "contact1" as first argument
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
    <div className="bg-[#F8F9FD]">
      <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-4 gap-x-2 mt-6">
        <StatusInputField
          className="xl:col-span-2"
          label="Street"
          name="street"
          type="text"
          status={getFieldStatus("street")}
          errorMessage={
            getFieldStatus("street") === "error" ? "This field is required" : ""
          }
          value={formData.street}
          onChange={(e) => handleInputChange("street", e.target.value)}
        />
        <StatusInputField
          label="Unit Number"
          name="unit"
          type="text"
          status={getFieldStatus("unit")}
          errorMessage={
            getFieldStatus("unit") === "error" ? "This field is required" : ""
          }
          value={formData.unit}
          onChange={(e) => handleInputChange("unit", e.target.value)}
        />
        <StatusInputField
          label="Block"
          name="block"
          type="text"
          status={getFieldStatus("block")}
          errorMessage={
            getFieldStatus("block") === "error" ? "This field is required" : ""
          }
          value={formData.block}
          onChange={(e) => handleInputChange("block", e.target.value)}
        />
        <StatusInputField
          label="City"
          name="city"
          type="text"
          status={getFieldStatus("city")}
          errorMessage={
            getFieldStatus("city") === "error" ? "This field is required" : ""
          }
          value={formData.city}
          onChange={(e) => handleInputChange("city", e.target.value)}
        />
        <StatusInputField
          label="State"
          name="state"
          type="text"
          status={getFieldStatus("state")}
          errorMessage={
            getFieldStatus("state") === "error" ? "This field is required" : ""
          }
          value={formData.state}
          onChange={(e) => handleInputChange("state", e.target.value)}
        />
        <StatusInputField
          label="Floor"
          name="floor"
          type="text"
          status={getFieldStatus("floor")}
          errorMessage={
            getFieldStatus("floor") === "error" ? "This field is required" : ""
          }
          value={formData.floor}
          onChange={(e) => handleInputChange("floor", e.target.value)}
        />
        <StatusInputField
          label="Postal Code"
          name="postal"
          type="text"
          status={getFieldStatus("postal")}
          errorMessage={
            getFieldStatus("postal") === "error" ? "This field is required" : ""
          }
          value={formData.postal}
          onChange={(e) => handleInputChange("postal", e.target.value)}
        />
        <StatusInputField
          label="Zone Location"
          name="zone"
          type="text"
          status={getFieldStatus("zone")}
          errorMessage={
            getFieldStatus("zone") === "error" ? "This field is required" : ""
          }
          value={formData.zone}
          onChange={(e) => handleInputChange("zone", e.target.value)}
        />
        <StatusInputField
          label="Country Code"
          name="countryCode"
          type="text"
          status={getFieldStatus("countryCode")}
          errorMessage={
            getFieldStatus("countryCode") === "error"
              ? "This field is required"
              : ""
          }
          value={formData.countryCode}
          onChange={(e) => handleInputChange("countryCode", e.target.value)}
        />
        <StatusInputField
          label="Area Code"
          name="areaCode"
          type="text"
          status={getFieldStatus("areaCode")}
          errorMessage={
            getFieldStatus("areaCode") === "error"
              ? "This field is required"
              : ""
          }
          value={formData.areaCode}
          onChange={(e) => handleInputChange("areaCode", e.target.value)}
        />
        <StatusInputField
          label="Company Main Contact Number"
          name="contact"
          type="text"
          status={getFieldStatus("contact")}
          errorMessage={
            getFieldStatus("contact") === "error"
              ? "This field is required"
              : ""
          }
          value={formData.contact}
          onChange={(e) => handleInputChange("contact", e.target.value)}
        />
      </div>
    </div>
  );
}
