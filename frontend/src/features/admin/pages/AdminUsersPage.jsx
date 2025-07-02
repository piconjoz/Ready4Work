// frontend/src/features/admin/pages/AdminUsersPage.jsx
import AdminHeader from "../../../components/AdminHeader";
import TableEntry from "../../../components/TableEntry";
import UserManagementModal from "../components/UserManagementModal";
import React, { useState, useEffect } from "react";
import { 
  getAllUsers, 
  getUsersByType, 
  deleteUserAccount, 
  toggleUserActivation,
  createUserAccount,
  updateUserAccount 
} from "../../../services/adminAPI";
import toast from "react-hot-toast";

export default function AdminUsersPage() {
  const [users, setUsers] = useState([]);
  const [filteredUsers, setFilteredUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedUserType, setSelectedUserType] = useState("all");
  const [showCreateModal, setShowCreateModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [selectedUser, setSelectedUser] = useState(null);

  useEffect(() => {
    loadUsers();
  }, []);

  useEffect(() => {
    // Filter users based on search term and user type
    let filtered = users;

    if (searchTerm) {
      filtered = filtered.filter(user => 
        user.fullName.toLowerCase().includes(searchTerm.toLowerCase()) ||
        user.email.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }

    if (selectedUserType !== "all") {
      const userTypeMap = { applicant: 1, recruiter: 2, admin: 3 };
      filtered = filtered.filter(user => user.userType === userTypeMap[selectedUserType]);
    }

    setFilteredUsers(filtered);
  }, [users, searchTerm, selectedUserType]);

  const loadUsers = async () => {
    setLoading(true);
    setError(null);

    try {
      const userData = await getAllUsers();
      setUsers(userData);
    } catch (error) {
      console.error("Error loading users:", error);
      setError("Failed to load users");
      toast.error("Failed to load users");
    } finally {
      setLoading(false);
    }
  };

  const handleDeleteUser = async (user) => {
    if (!confirm(`Are you sure you want to delete ${user.fullName}? This action cannot be undone.`)) {
      return;
    }

    try {
      await deleteUserAccount(user.userId);
      toast.success(`${user.fullName} has been deleted`);
      loadUsers(); // Reload the list
    } catch (error) {
      console.error("Error deleting user:", error);
      if (error.response?.data?.message) {
        toast.error(error.response.data.message);
      } else {
        toast.error("Failed to delete user");
      }
    }
  };

  const handleToggleUserStatus = async (user) => {
    const newStatus = !user.isActive;
    const action = newStatus ? "activate" : "deactivate";

    if (!confirm(`Are you sure you want to ${action} ${user.fullName}?`)) {
      return;
    }

    try {
      await toggleUserActivation(user.userId, newStatus);
      toast.success(`${user.fullName} has been ${action}d`);
      loadUsers(); // Reload the list
    } catch (error) {
      console.error("Error toggling user status:", error);
      if (error.response?.data?.message) {
        toast.error(error.response.data.message);
      } else {
        toast.error(`Failed to ${action} user`);
      }
    }
  };

  const handleEditUser = (user) => {
    setSelectedUser(user);
    setShowEditModal(true);
  };

  const handleSearch = (searchTerm) => {
    setSearchTerm(searchTerm);
  };

  const getUserTypeDisplay = (userType) => {
    const types = { 1: "Applicant", 2: "Recruiter", 3: "Admin" };
    return types[userType] || "Unknown";
  };

  const getUserAvatar = (user) => {
    // Generate a simple avatar based on initials
    const initials = `${user.fullName.split(' ')[0]?.[0] || ''}${user.fullName.split(' ')[1]?.[0] || ''}`;
    return `https://ui-avatars.com/api/?name=${initials}&background=random&color=fff&size=40`;
  };

  const columns = [
    {
      header: "User",
      accessor: "name",
      render: (user) => (
        <div className="flex items-center mr-5">
          <img 
            className="w-10 h-10 rounded-full" 
            src={getUserAvatar(user)} 
            alt={`${user.fullName} avatar`} 
          />
          <div className="ps-3">
            <div className="text-base text-black font-medium">{user.fullName}</div>
            <div className="font-normal text-gray-500">{getUserTypeDisplay(user.userType)}</div>
          </div>
        </div>
      )
    },
    { 
      header: "Email", 
      accessor: "email",
      render: (user) => (
        <div>
          <div className="text-sm font-medium">{user.email}</div>
          <div className="text-xs text-gray-500">
            Joined {new Date(user.createdAt).toLocaleDateString()}
          </div>
        </div>
      )
    },
    {
      header: "Status",
      accessor: "status",
      render: (user) => (
        <div className="space-y-1">
          <span className={`inline-flex px-2 py-1 rounded-full text-xs font-medium ${
            user.isActive 
              ? 'bg-green-100 text-green-800' 
              : 'bg-red-100 text-red-800'
          }`}>
            {user.isActive ? 'Active' : 'Inactive'}
          </span>
          {user.isVerified && (
            <span className="block px-2 py-1 rounded-full text-xs font-medium bg-blue-100 text-blue-800">
              Verified
            </span>
          )}
        </div>
      )
    },
    {
      header: "Actions",
      accessor: "",
      render: (user) => (
        <div className="space-x-2">
          <button 
            onClick={() => handleEditUser(user)} 
            className="font-medium text-blue-600 hover:text-blue-800 hover:underline"
          >
            Edit
          </button>
          <button 
            onClick={() => handleToggleUserStatus(user)} 
            className={`font-medium hover:underline ${
              user.isActive ? 'text-orange-600 hover:text-orange-800' : 'text-green-600 hover:text-green-800'
            }`}
          >
            {user.isActive ? 'Deactivate' : 'Activate'}
          </button>
          <button 
            onClick={() => handleDeleteUser(user)} 
            className="font-medium text-red-600 hover:text-red-800 hover:underline"
          >
            Delete
          </button>
        </div>
      )
    }
  ];

  if (loading) {
    return (
      <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
        <AdminHeader active="users" />
        <div className="pt-6">
          <div className="flex items-center justify-center h-64">
            <div className="text-lg">Loading users...</div>
          </div>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
        <AdminHeader active="users" />
        <div className="pt-6">
          <div className="bg-red-50 border border-red-200 rounded-lg p-4">
            <h3 className="text-red-800 font-medium">Error Loading Users</h3>
            <p className="text-red-600 text-sm mt-1">{error}</p>
            <button 
              onClick={loadUsers}
              className="mt-3 px-4 py-2 bg-red-600 text-white rounded-lg text-sm hover:bg-red-700"
            >
              Retry
            </button>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
      <AdminHeader active="users" />

      <div className="pt-6">
        <div className="flex justify-between items-center mb-6">
          <div>
            <h1 className="text-2xl font-semibold">User Management</h1>
            <p className="text-gray-600 text-sm mt-1">
              Manage all system users ({filteredUsers.length} of {users.length} shown)
            </p>
          </div>
          <button
            onClick={() => setShowCreateModal(true)}
            className="px-4 py-2 bg-black text-white rounded-lg text-sm hover:bg-gray-800"
          >
            Create User
          </button>
        </div>

        {/* Filters */}
        <div className="bg-white rounded-lg border border-gray-200 p-4 mb-6">
          <div className="flex gap-4 items-center">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Filter by Type
              </label>
              <select
                value={selectedUserType}
                onChange={(e) => setSelectedUserType(e.target.value)}
                className="border border-gray-300 rounded-lg px-3 py-2 text-sm"
              >
                <option value="all">All Users</option>
                <option value="applicant">Applicants</option>
                <option value="recruiter">Recruiters</option>
                <option value="admin">Administrators</option>
              </select>
            </div>
            <div className="flex-1">
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Search
              </label>
              <input
                type="text"
                placeholder="Search by name or email..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm"
              />
            </div>
          </div>
        </div>

        {/* Users Table */}
        <div className="bg-white rounded-lg border border-gray-200 overflow-hidden">
          <table className="w-full text-sm text-left text-gray-500">
            <thead className="text-xs text-black uppercase bg-gray-50">
              <tr>
                {columns.map((col, idx) => (
                  <th
                    key={idx}
                    className={`px-6 py-3 text-left whitespace-nowrap ${col.className ?? ''}`}
                  >
                    {col.header}
                  </th>
                ))}
              </tr>
            </thead>
            <tbody>
              {filteredUsers.map((user, idx) => (
                <tr
                  key={idx}
                  className="border-b border-gray-300 hover:bg-gray-50"
                >
                  {columns.map((col, cIdx) => (
                    <td
                      key={cIdx}
                      className={`px-6 py-4 text-left whitespace-nowrap ${col.className ?? ''}`}
                    >
                      {col.render ? col.render(user) : user[col.accessor]}
                    </td>
                  ))}
                </tr>
              ))}
            </tbody>
          </table>
        </div>

        {/* User Management Modal */}
        <UserManagementModal
          isOpen={showCreateModal || showEditModal}
          onClose={() => {
            setShowCreateModal(false);
            setShowEditModal(false);
            setSelectedUser(null);
          }}
          user={selectedUser}
          onSuccess={loadUsers}
        />
      </div>
    </div>
  );
}