import React, { useState, useEffect } from "react";
import AdminHeader from "../../../components/AdminHeader";
import StatCard from "../../../components/StatisticCard";
import { 
  getAdminDashboard, 
  getSystemInfo, 
  getAllUsers, 
  getAllCompanies 
} from "../../../services/adminAPI";
import { FaUsers, FaBuilding, FaClipboardList, FaUserShield } from "react-icons/fa";
import { MdAccessTime, MdStorage, MdSecurity } from "react-icons/md";
import toast from "react-hot-toast";

export default function AdminDashboardPage() {
  const [dashboardData, setDashboardData] = useState(null);
  const [systemInfo, setSystemInfo] = useState(null);
  const [users, setUsers] = useState([]);
  const [companies, setCompanies] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    loadDashboardData();
  }, []);

  const loadDashboardData = async () => {
    setLoading(true);
    setError(null);

    try {
      // Load all dashboard data concurrently
      const [dashboardResponse, systemResponse, usersResponse, companiesResponse] = 
        await Promise.allSettled([
          getAdminDashboard(),
          getSystemInfo(),
          getAllUsers(),
          getAllCompanies()
        ]);

      // Handle dashboard data
      if (dashboardResponse.status === 'fulfilled') {
        setDashboardData(dashboardResponse.value);
      } else {
        console.error('Dashboard data failed:', dashboardResponse.reason);
      }

      // Handle system info
      if (systemResponse.status === 'fulfilled') {
        setSystemInfo(systemResponse.value);
      } else {
        console.error('System info failed:', systemResponse.reason);
      }

      // Handle users data
      if (usersResponse.status === 'fulfilled') {
        setUsers(usersResponse.value);
      } else {
        console.error('Users data failed:', usersResponse.reason);
      }

      // Handle companies data
      if (companiesResponse.status === 'fulfilled') {
        setCompanies(companiesResponse.value);
      } else {
        console.error('Companies data failed:', companiesResponse.reason);
      }

    } catch (error) {
      console.error("Error loading dashboard data:", error);
      setError("Failed to load dashboard data");
      toast.error("Failed to load dashboard data");
    } finally {
      setLoading(false);
    }
  };

  // Calculate user type counts
  const userCounts = users.reduce(
    (acc, user) => {
      switch (user.userType) {
        case 1:
          acc.applicants++;
          break;
        case 2:
          acc.recruiters++;
          break;
        case 3:
          acc.admins++;
          break;
      }
      return acc;
    },
    { applicants: 0, recruiters: 0, admins: 0 }
  );

  const activeUsers = users.filter(user => user.isActive).length;
  const verifiedUsers = users.filter(user => user.isVerified).length;

  if (loading) {
    return (
      <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
        <AdminHeader active="dashboard" />
        <div className="pt-6">
          <div className="flex items-center justify-center h-64">
            <div className="text-lg">Loading dashboard...</div>
          </div>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
        <AdminHeader active="dashboard" />
        <div className="pt-6">
          <div className="bg-red-50 border border-red-200 rounded-lg p-4">
            <h3 className="text-red-800 font-medium">Error Loading Dashboard</h3>
            <p className="text-red-600 text-sm mt-1">{error}</p>
            <button 
              onClick={loadDashboardData}
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
      <AdminHeader active="dashboard" />

      <div className="pt-6">
        <div className="flex justify-between items-center mb-6">
          <h1 className="text-2xl font-semibold">Admin Dashboard</h1>
          <button 
            onClick={loadDashboardData}
            className="px-4 py-2 bg-black text-white rounded-lg text-sm hover:bg-gray-800"
          >
            Refresh Data
          </button>
        </div>

        {/* Overview Statistics */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
          <StatCard
            icon={<FaUsers className="w-5 h-5" />}
            value={users.length}
            label="Total Users"
          />
          <StatCard
            icon={<FaBuilding className="w-5 h-5" />}
            value={companies.length}
            label="Total Companies"
          />
          <StatCard
            icon={<FaClipboardList className="w-5 h-5" />}
            value={activeUsers}
            label="Active Users"
          />
          <StatCard
            icon={<FaUserShield className="w-5 h-5" />}
            value={verifiedUsers}
            label="Verified Users"
          />
        </div>

        {/* User Types Breakdown */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mb-8">
          <StatCard
            icon={<FaUsers className="w-5 h-5" />}
            value={userCounts.applicants}
            label="Applicants"
          />
          <StatCard
            icon={<FaBuilding className="w-5 h-5" />}
            value={userCounts.recruiters}
            label="Recruiters"
          />
          <StatCard
            icon={<FaUserShield className="w-5 h-5" />}
            value={userCounts.admins}
            label="Administrators"
          />
        </div>

        {/* System Information */}
        {systemInfo && (
          <div className="bg-white rounded-2xl border border-gray-200 p-6 mb-8">
            <h2 className="text-xl font-semibold mb-4">System Information</h2>
            <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
              <div className="flex items-center gap-3">
                <MdAccessTime className="w-5 h-5 text-blue-600" />
                <div>
                  <div className="text-sm text-gray-500">Server Time</div>
                  <div className="font-medium">
                    {new Date(systemInfo.serverTime).toLocaleString()}
                  </div>
                </div>
              </div>
              <div className="flex items-center gap-3">
                <MdStorage className="w-5 h-5 text-green-600" />
                <div>
                  <div className="text-sm text-gray-500">Environment</div>
                  <div className="font-medium">{systemInfo.environment}</div>
                </div>
              </div>
              <div className="flex items-center gap-3">
                <MdSecurity className="w-5 h-5 text-purple-600" />
                <div>
                  <div className="text-sm text-gray-500">Database Status</div>
                  <div className="font-medium">{systemInfo.databaseStatus}</div>
                </div>
              </div>
            </div>
          </div>
        )}

        {/* Recent Activity */}
        <div className="bg-white rounded-2xl border border-gray-200 p-6">
          <h2 className="text-xl font-semibold mb-4">Recent Activity</h2>
          <div className="space-y-4">
            {users.slice(0, 5).map((user) => (
              <div key={user.userId} className="flex items-center justify-between border-b border-gray-100 pb-3 last:border-b-0">
                <div>
                  <div className="font-medium">{user.fullName}</div>
                  <div className="text-sm text-gray-500">
                    {user.userTypeDisplay} • Created {new Date(user.createdAt).toLocaleDateString()}
                  </div>
                </div>
                <div className="flex gap-2">
                  <span className={`px-2 py-1 rounded-full text-xs ${
                    user.isActive 
                      ? 'bg-green-100 text-green-800' 
                      : 'bg-gray-100 text-gray-800'
                  }`}>
                    {user.isActive ? 'Active' : 'Inactive'}
                  </span>
                  {user.isVerified && (
                    <span className="px-2 py-1 rounded-full text-xs bg-blue-100 text-blue-800">
                      Verified
                    </span>
                  )}
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}