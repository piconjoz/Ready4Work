import { useState } from "react";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";
import StatusInputField from "../../../../components/StatusInputField";
import SelectField from "../../../../components/SelectField";
import PrimaryButton from "../../../../components/PrimaryButton";
import { onboardRecruiter } from "../../../../services/authApi";

export default function SummaryForm({
  signupData = {},
  companyData = {},
  contactData = {},
  recruiterData = {},
  onEmailChange,
}) {
  const navigate = useNavigate();
  const [editableEmail, setEditableEmail] = useState(signupData?.email || "");
  const [gender, setGender] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [validationErrors, setValidationErrors] = useState({});

  const genderOptions = [
    { value: "", label: "Select Gender" },
    { value: "Male", label: "Male" },
    { value: "Female", label: "Female" },
  ];

  const handleEmailChange = (e) => {
    const newEmail = e.target.value;
    setEditableEmail(newEmail);

    // Clear validation error when user starts typing
    if (validationErrors.email) {
      setValidationErrors((prev) => ({ ...prev, email: false }));
    }

    if (onEmailChange) {
      onEmailChange(newEmail);
    }
  };

  const handleGenderChange = (e) => {
    setGender(e.target.value);

    // Clear validation error when user selects gender
    if (validationErrors.gender) {
      setValidationErrors((prev) => ({ ...prev, gender: false }));
    }
  };

  const validateForm = () => {
    const errors = {};

    if (!editableEmail.trim()) {
      errors.email = true;
      toast.error("Please enter an email address");
    }

    if (!gender) {
      errors.gender = true;
      toast.error("Please select a gender");
    }

    setValidationErrors(errors);
    return Object.keys(errors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validateForm()) {
      return;
    }

    setIsSubmitting(true);

    // API payload matching your backend expectation
    const apiPayload = {
      company: {
        companyName: companyData.registeredName || "Pokka Singapore",
        preferredCompanyName: companyData.preferredName || "Pokka SG",
        companyDescription:
          companyData.profile ||
          "Pokka Singapore is a leading beverage manufacturer...",
        countryOfBusinessRegistration: companyData.country || "Singapore",
        uen: companyData.uen || "2020",
        numberOfEmployees: parseInt(companyData.employees) || 10,
        industryCluster: companyData.industry || "Food Manufacturing",
        entityType:
          companyData.entityType || "Non-Governmental Organisations (NGOs)...",
        authorisedTrainingOrganisation:
          (companyData.ato || "Yes").toLowerCase() === "yes",
        companyWebsite: companyData.website || "www.pokka.com.sg",
        companyContact: contactData.contact || "68363371",
        city: contactData.city || "Singapore",
        state: contactData.state || "Singapore",
        zoneLocation: contactData.zone || "North",
        countryCode: parseInt(contactData.countryCode) || 65,
        unitNumber: contactData.unit || "01#135",
        floor: contactData.floor || "25",
        areaCode: parseInt(contactData.areaCode) || 0,
        block: contactData.block || "5A",
        postalCode: parseInt(contactData.postal) || 736153,
        employmentType:
          companyData.employmentType ||
          "IWSP – Integrated Work Study Programme...",
      },
      user: {
        email: editableEmail,
        firstName: recruiterData.firstName || "Dave",
        lastName: recruiterData.lastName || "Johnson",
        phone: recruiterData.mobileNumber || "98646153",
        gender: gender,
        userType: 2,
        password: signupData.password || "",
      },
      recruiter: {
        jobTitle: recruiterData.designation || "CFO",
        department: recruiterData.department || "Research Development",
      },
      metadata: {
        registrationDate:
          signupData?.registrationDate ||
          new Date().toISOString().split("T")[0],
        submissionTimestamp: new Date().toISOString(),
        accountType: "Employer",
      },
    };

    try {
      // console.log("SENDING API PAYLOAD:", apiPayload);
      const response = await onboardRecruiter(apiPayload);
      // console.log("API RESPONSE SUCCESS:", response);

      // Extract email from response for the toast message
      const responseEmail = response?.email || editableEmail;

      toast.success(
        `Registration completed successfully! Please login with ${responseEmail}`,
        {
          duration: 3000,
        }
      );

      // Redirect to login page after toast shows
      setTimeout(() => {
        navigate("/auth/login");
      }, 3000);
    } catch (error) {
      console.error("API RESPONSE ERROR:", error);

      // Handle different error types with specific toast messages
      if (error.response?.status === 409) {
        toast.error(
          "Company or user already exists. Please check your details."
        );
      } else if (error.response?.status === 400) {
        toast.error("Invalid data provided. Please check all fields.");
      } else if (error.response?.data?.message) {
        toast.error(error.response.data.message);
      } else if (error.message) {
        toast.error(error.message);
      } else {
        toast.error("Registration failed. Please try again.");
      }
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        {/* Company Information Section */}
        <div className="mb-8">
          <h3 className="text-lg font-semibold mb-4">Company Information</h3>
          <div className="grid grid-cols-1 xl:grid-cols-4 gap-x-2">
            <StatusInputField
              label="Registered Company Name"
              name="registeredName"
              type="text"
              status="default"
              defaultValue={companyData.registeredName || "Pokka Singapore"}
              readOnly
            />
            <StatusInputField
              label="Preferred Company Name"
              name="preferredName"
              type="text"
              status="default"
              defaultValue={companyData.preferredName || "Pokka SG"}
              readOnly
            />
            <StatusInputField
              label="Country of Business Registration"
              name="country"
              type="text"
              status="default"
              defaultValue={companyData.country || "Singapore"}
              readOnly
            />
            <StatusInputField
              label="UEN"
              name="uen"
              type="text"
              status="default"
              defaultValue={companyData.uen || "2020"}
              readOnly
            />
            <StatusInputField
              label="Number of Employees"
              name="employees"
              type="text"
              status="default"
              defaultValue={companyData.employees || "10"}
              readOnly
            />
            <StatusInputField
              label="Industry Cluster"
              name="industry"
              type="text"
              status="default"
              defaultValue={companyData.industry || "Food Manufacturing"}
              readOnly
            />
            <StatusInputField
              className="xl:col-span-2"
              label="Entity Type"
              name="entityType"
              type="text"
              status="default"
              defaultValue={
                companyData.entityType ||
                "Non-Governmental Organisations (NGOs), Non-Profit Organisations (NPO), Social Service Agencies (SSA)"
              }
              readOnly
            />
            <StatusInputField
              label="Authorised Training Organisation"
              name="ato"
              type="text"
              status="default"
              defaultValue={companyData.ato || "Yes"}
              readOnly
            />
            <StatusInputField
              label="Company Website"
              name="website"
              type="text"
              status="default"
              defaultValue={companyData.website || "www.pokka.com.sg"}
              readOnly
            />
            <StatusInputField
              className="xl:col-span-2"
              label="Employment Type Interested In"
              name="employmentType"
              type="text"
              status="default"
              defaultValue={
                companyData.employmentType ||
                "IWSP – Integrated Work Study Programme / TITO – Term-In Term-Out / TITO-IWL – Integrated Workplace Learning"
              }
              readOnly
            />
            <StatusInputField
              className="col-span-full"
              label="Company Profile"
              name="profile"
              type="textarea"
              status="default"
              defaultValue={
                companyData.profile ||
                "Pokka Singapore is a leading beverage manufacturer and distributor known for its wide range of ready-to-drink coffee, teas, juices, and functional drinks. Established in 1977, Pokka is headquartered in Singapore and has built a strong presence across Asia and the Middle East. The company is committed to high-quality production, innovation, and health-conscious formulations, with many of its beverages brewed using authentic methods. As part of the Pokka Sapporo Food & Beverage group from Japan, Pokka Singapore combines Japanese quality with local taste preferences, making it a household name in the region's beverage industry."
              }
              readOnly
            />
          </div>
        </div>

        {/* Company Contact Information Section */}
        <div className="mb-8">
          <h3 className="text-lg font-semibold mb-4">
            Company Contact Information
          </h3>
          <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-4 gap-x-2">
            <StatusInputField
              className="xl:col-span-2"
              label="Street"
              name="street"
              type="text"
              status="default"
              defaultValue={contactData.street || "1 Punggol Coast Road"}
              readOnly
            />
            <StatusInputField
              label="Unit Number"
              name="unit"
              type="text"
              status="default"
              defaultValue={contactData.unit || "01#135"}
              readOnly
            />
            <StatusInputField
              label="Block"
              name="block"
              type="text"
              status="default"
              defaultValue={contactData.block || "5A"}
              readOnly
            />
            <StatusInputField
              label="City"
              name="city"
              type="text"
              status="default"
              defaultValue={contactData.city || "Singapore"}
              readOnly
            />
            <StatusInputField
              label="State"
              name="state"
              type="text"
              status="default"
              defaultValue={contactData.state || "Singapore"}
              readOnly
            />
            <StatusInputField
              label="Floor"
              name="floor"
              type="text"
              status="default"
              defaultValue={contactData.floor || "25"}
              readOnly
            />
            <StatusInputField
              label="Postal Code"
              name="postal"
              type="text"
              status="default"
              defaultValue={contactData.postal || "736153"}
              readOnly
            />
            <StatusInputField
              label="Zone Location"
              name="zone"
              type="text"
              status="default"
              defaultValue={contactData.zone || "North"}
              readOnly
            />
            <StatusInputField
              label="Country Code"
              name="countryCode"
              type="text"
              status="default"
              defaultValue={contactData.countryCode || "65"}
              readOnly
            />
            <StatusInputField
              label="Area Code"
              name="areaCode"
              type="text"
              status="default"
              defaultValue={contactData.areaCode || "-"}
              readOnly
            />
            <StatusInputField
              label="Company Main Contact Number"
              name="contact"
              type="text"
              status="default"
              defaultValue={contactData.contact || "68363371"}
              readOnly
            />
          </div>
        </div>

        {/* Business Contact Information Section */}
        <div className="mb-8">
          <h3 className="text-lg font-semibold mb-4">
            Business Contact Information
          </h3>
          <div className="grid grid-cols-1 xl:grid-cols-4 gap-x-2">
            <StatusInputField
              label="Designated Account Email"
              name="accountEmail"
              type="email"
              status={validationErrors.email ? "error" : "default"}
              value={editableEmail}
              onChange={handleEmailChange}
            />

            {/* Gender Dropdown */}
            <div>
              <SelectField
                label="Gender"
                name="gender"
                value={gender}
                onChange={handleGenderChange}
                options={genderOptions}
                status={validationErrors.gender ? "error" : "default"}
              />
            </div>

            <StatusInputField
              label="Account Type"
              name="accountType"
              type="text"
              status="default"
              defaultValue="Employer"
              readOnly
            />
            <StatusInputField
              label="Registration Date"
              name="registrationDate"
              type="text"
              status="default"
              defaultValue={
                signupData?.registrationDate ||
                new Date().toISOString().split("T")[0]
              }
              readOnly
            />

            <StatusInputField
              label="First Name"
              name="firstName"
              type="text"
              status="default"
              defaultValue={recruiterData.firstName || "Dave"}
              readOnly
            />
            <StatusInputField
              label="Last Name"
              name="lastName"
              type="text"
              status="default"
              defaultValue={recruiterData.lastName || "Johnson"}
              readOnly
            />
            <StatusInputField
              label="Salutations"
              name="salutation"
              type="text"
              status="default"
              defaultValue={recruiterData.salutation || "Mr"}
              readOnly
            />
            <StatusInputField
              label="Department"
              name="department"
              type="text"
              status="default"
              defaultValue={recruiterData.department || "Research Development"}
              readOnly
            />
            <StatusInputField
              className="xl:col-span-2"
              label="Full Legal Name"
              name="fullLegalName"
              type="text"
              status="default"
              defaultValue={
                recruiterData.fullLegalName || "Dave Timothy Johnson"
              }
              readOnly
            />
            <StatusInputField
              label="Office Contact Country Code"
              name="officeCode"
              type="text"
              status="default"
              defaultValue={recruiterData.officeCode || "65"}
              readOnly
            />
            <StatusInputField
              label="Office Contact Number"
              name="officeNumber"
              type="text"
              status="default"
              defaultValue={recruiterData.officeNumber || "68363371"}
              readOnly
            />
            <StatusInputField
              label="Designation"
              name="designation"
              type="text"
              status="default"
              defaultValue={recruiterData.designation || "CFO"}
              readOnly
            />
            <StatusInputField
              label="Business Contact Email"
              name="businessEmail"
              type="email"
              status="default"
              defaultValue={recruiterData.email || "Dave@pokka.com.sg"}
              readOnly
            />
            <StatusInputField
              label="Mobile Contact Country Code"
              name="mobileCode"
              type="text"
              status="default"
              defaultValue={recruiterData.mobileCode || "65"}
              readOnly
            />
            <StatusInputField
              label="Mobile Contact Number"
              name="mobileNumber"
              type="text"
              status="default"
              defaultValue={recruiterData.mobileNumber || "98646153"}
              readOnly
            />
          </div>
        </div>

        {/* Submit Button */}
        <div className="mt-8">
          <PrimaryButton
            type="submit"
            label={isSubmitting ? "Submitting..." : "Complete Registration"}
            className=""
            disabled={isSubmitting}
          />
        </div>
      </div>
    </form>
  );
}
