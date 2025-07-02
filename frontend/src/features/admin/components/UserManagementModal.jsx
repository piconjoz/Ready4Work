// frontend/src/features/admin/components/UserManagementModal.jsx
import React, { useState, useEffect } from "react";
import StatusInputField from "../../../components/StatusInputField";
import PrimaryButton from "../../../components/PrimaryButton";
import { updateUserAccount } from "../../../services/adminAPI";
import toast from "react-hot-toast";

export default function UserManagementModal({ 
  isOpen, 
  onClose, 
  user = null, 
  onSuccess 
}) {
  // This modal now only handles editing existing users
  if (!user) {
    console.warn("UserManagementModal: No user provided for editing");
    return null;
  }

  const [loading, setLoading] = useState(false);
  const [formData, setFormData] = useState({
    email: "",
    firstName: "",
    lastName: "",
    phone: "",
    gender: "",
    userType: 1,
    isActive: true,
    isVerified: false,
    // Role-specific data
    applicantData: {
      programmeId: 1,
      admitYear: new Date().getFullYear()
    },
    recruiterData: {
      companyId: 1,
      jobTitle: "",
      department: ""
    }
  });

  const [errors, setErrors] = useState({});

  useEffect(() => {
    if (user) {
      setFormData({
        email: user.email || "",
        firstName: user.fullName?.split(' ')[0] || "",
        lastName: user.fullName?.split(' ').slice(1).join(' ') || "",
        phone: "",
        gender: "",
        userType: user.userType || 1,
        isActive: user.isActive ?? true,
        isVerified: user.isVerified ?? false,
        applicantData: {
          programmeId: user.roleData?.programmeId || 1,
          admitYear: user.roleData?.admitYear || new Date().getFullYear()
        },
        recruiterData: {
          companyId: user.roleData?.companyId || 1,
          jobTitle: user.roleData?.jobTitle || "",
          department: user.roleData?.department || ""
        }
      });
    }
    setErrors({});
  }, [user, isOpen]);

  const handleInputChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value
    }));
    
    // Clear error when user starts typing
    if (errors[name]) {
      setErrors(prev => ({ ...prev, [name]: "" }));
    }
  };

  const handleRoleDataChange = (field, value) => {
    const roleKey = formData.userType === 1 ? 'applicantData' : 'recruiterData';
    setFormData(prev => ({
      ...prev,
      [roleKey]: {
        ...prev[roleKey],
        [field]: value
      }
    }));
  };

  const validateForm = () => {
    const newErrors = {};

    if (!formData.email.trim()) newErrors.email = "Email is required";
    else if (!/\S+@\S+\.\S+/.test(formData.email)) newErrors.email = "Invalid email format";

    if (!formData.firstName.trim()) newErrors.firstName = "First name is required";
    if (!formData.lastName.trim()) newErrors.lastName = "Last name is required";

    // Role-specific validation
    if (formData.userType === 1) {
      if (!formData.applicantData.programmeId) {
        newErrors.programmeId = "Programme is required";
      }
      if (!formData.applicantData.admitYear) {
        newErrors.admitYear = "Admit year is required";
      }
    } else if (formData.userType === 2) {
      if (!formData.recruiterData.jobTitle.trim()) {
        newErrors.jobTitle = "Job title is required";
      }
      if (!formData.recruiterData.companyId) {
        newErrors.companyId = "Company is required";
      }
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (!validateForm()) return;

    setLoading(true);
    try {
      const userData = {
        firstName: formData.firstName,
        lastName: formData.lastName,
        phone: formData.phone || null,
        gender: formData.gender || null,
        isActive: formData.isActive,
        isVerified: formData.isVerified
      };

      // Add role-specific data
      if (formData.userType === 1) {
        userData.applicantData = formData.applicantData;
      } else if (formData.userType === 2) {
        userData.recruiterData = formData.recruiterData;
      }

      await updateUserAccount(user.userId, userData);
      toast.success("User updated successfully");

      onSuccess();
      onClose();
    } catch (error) {
      console.error("Error updating user:", error);
      if (error.response?.data?.message) {
        toast.error(error.response.data.message);
      } else {
        toast.error("Failed to update user");
      }
    } finally {
      setLoading(false);
    }
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/60 px-4">
      <div className="bg-white w-full max-w-2xl max-h-[90vh] rounded-xl p-6 overflow-y-auto shadow-xl">
        {/* Header */}
        <div className="flex justify-between items-center mb-6">
          <h2 className="text-xl font-bold">Edit User</h2>
          <button 
            onClick={onClose} 
            className="bg-gray-100 hover:bg-gray-200 text-gray-800 px-4 py-2 rounded-lg text-sm"
          >
            Cancel
          </button>
        </div>

        <form onSubmit={handleSubmit} className="space-y-4">
          {/* Basic Information */}
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Email
              </label>
              <input
                type="email"
                value={formData.email}
                readOnly
                className="w-full px-3 py-2 border border-gray-300 rounded-lg bg-gray-50 text-gray-500 cursor-not-allowed"
              />
            </div>
            
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                First Name
              </label>
              <input
                type="text"
                name="firstName"
                value={formData.firstName}
                onChange={handleInputChange}
                className={`w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-black focus:border-transparent ${
                  errors.firstName ? 'border-red-500' : 'border-gray-300'
                }`}
                placeholder="Enter first name"
              />
              {errors.firstName && (
                <p className="text-red-500 text-sm mt-1">{errors.firstName}</p>
              )}
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Last Name
              </label>
              <input
                type="text"
                name="lastName"
                value={formData.lastName}
                onChange={handleInputChange}
                className={`w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-black focus:border-transparent ${
                  errors.lastName ? 'border-red-500' : 'border-gray-300'
                }`}
                placeholder="Enter last name"
              />
              {errors.lastName && (
                <p className="text-red-500 text-sm mt-1">{errors.lastName}</p>
              )}
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Phone (Optional)
              </label>
              <input
                type="text"
                name="phone"
                value={formData.phone}
                onChange={handleInputChange}
                className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-black focus:border-transparent"
                placeholder="Enter phone number"
              />
            </div>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Gender
              </label>
              <select
                name="gender"
                value={formData.gender}
                onChange={handleInputChange}
                className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-black focus:border-transparent"
              >
                <option value="">Select Gender</option>
                <option value="Male">Male</option>
                <option value="Female">Female</option>
                <option value="Other">Other</option>
              </select>
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                User Type
              </label>
              <input
                type="text"
                value={formData.userType === 1 ? "Applicant" : formData.userType === 2 ? "Recruiter" : "Admin"}
                readOnly
                className="w-full px-3 py-2 border border-gray-300 rounded-lg bg-gray-50 text-gray-500 cursor-not-allowed"
              />
            </div>
          </div>

          {/* User Status */}
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div className="flex items-center gap-4">
              <label className="flex items-center gap-2">
                <input
                  type="checkbox"
                  name="isActive"
                  checked={formData.isActive}
                  onChange={handleInputChange}
                  className="accent-black"
                />
                <span className="text-sm">Active Account</span>
              </label>
            </div>
            <div className="flex items-center gap-4">
              <label className="flex items-center gap-2">
                <input
                  type="checkbox"
                  name="isVerified"
                  checked={formData.isVerified}
                  onChange={handleInputChange}
                  className="accent-black"
                />
                <span className="text-sm">Verified Account</span>
              </label>
            </div>
          </div>

          {/* Role-specific fields */}
          {formData.userType === 1 && (
            <div className="border-t pt-4">
              <h3 className="font-medium mb-3">Applicant Information</h3>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <StatusInputField
                  label="Programme ID"
                  name="programmeId"
                  type="number"
                  value={formData.applicantData.programmeId}
                  onChange={(e) => handleRoleDataChange('programmeId', parseInt(e.target.value))}
                  status={errors.programmeId ? "error" : "default"}
                  errorMessage={errors.programmeId}
                />
                <StatusInputField
                  label="Admit Year"
                  name="admitYear"
                  type="number"
                  value={formData.applicantData.admitYear}
                  onChange={(e) => handleRoleDataChange('admitYear', parseInt(e.target.value))}
                  status={errors.admitYear ? "error" : "default"}
                  errorMessage={errors.admitYear}
                />
              </div>
            </div>
          )}

          {formData.userType === 2 && (
            <div className="border-t pt-4">
              <h3 className="font-medium mb-3">Recruiter Information</h3>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <StatusInputField
                  label="Company ID"
                  name="companyId"
                  type="number"
                  value={formData.recruiterData.companyId}
                  onChange={(e) => handleRoleDataChange('companyId', parseInt(e.target.value))}
                  status={errors.companyId ? "error" : "default"}
                  errorMessage={errors.companyId}
                />
                <StatusInputField
                  label="Job Title"
                  name="jobTitle"
                  value={formData.recruiterData.jobTitle}
                  onChange={(e) => handleRoleDataChange('jobTitle', e.target.value)}
                  status={errors.jobTitle ? "error" : "default"}
                  errorMessage={errors.jobTitle}
                />
                <StatusInputField
                  label="Department (Optional)"
                  name="department"
                  value={formData.recruiterData.department}
                  onChange={(e) => handleRoleDataChange('department', e.target.value)}
                />
              </div>
            </div>
          )}

          {/* Submit Button */}
          <div className="flex gap-3 pt-4">
            <PrimaryButton
              type="submit"
              label={loading ? "Updating..." : "Update User"}
              disabled={loading}
              className="flex-1"
            />
            <button
              type="button"
              onClick={onClose}
              className="px-6 py-3 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50"
            >
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}