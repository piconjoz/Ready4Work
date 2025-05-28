function LoginPage() {
  return (
    <div className="bg-[#F8F9FD] flex justify-center items-center h-screen">
      {/* Left: Image */}
      <div className="w-3/5 h-screen hidden lg:block">
        <img
          /*src="https://www.woha.net/prod/wp-content/uploads/2025/02/01-SITCampus_Round1_ShiyaStudio-17.jpg"*/
          src="https://www.cmgassets.com/s3fs-public/users/user12663/sit-punggol-campus_campus-court_garden-and-waterfront.jpg?s1391429d1728288210"
          alt="Placeholder"
          className="object-cover w-full h-full"
        />
      </div>
      {/* Right: Login Form */}
      <div className="lg:p-10 md:p-40 sm:p-20 p-8 w-full lg:w-2/5 bg-[#F8F9FD]">
        <h1 className="text-2xl font-semibold mb-4 font-inter-tight">Welcome to Ready4Work.</h1>
        <form action="#" method="POST">
          {/* Username Input */}
          <div className="mb-4">
            <label htmlFor="username" className="block text-gray-600">
              Username
            </label>
            <input
              type="text"
              id="username"
              name="username"
              className="w-[95%] border border-gray-300 rounded-md py-2 px-3 focus:outline-none focus:border-blue-500"
              autoComplete="off"
            />
          </div>
          {/* Password Input */}
          <div className="mb-4">
            <label htmlFor="password" className="block text-gray-800">
              Password
            </label>
            <input
              type="password"
              id="password"
              name="password"
              className="w-[95%] border border-gray-300 rounded-md py-2 px-3 focus:outline-none focus:border-blue-500"
              autoComplete="off"
            />
          </div>
          {/* Remember Me Checkbox */}
          <div className="mb-4 flex items-center">
            <input
              type="checkbox"
              id="remember"
              name="remember"
              className="text-red-500"
            />
            <label htmlFor="remember" className="text-green-900 ml-2">
              Remember Me
            </label>
          </div>
          {/* Forgot Password Link */}
          <div className="mb-6 text-blue-500">
            <a href="#" className="hover:underline">
              Forgot Password?
            </a>
          </div>
          {/* Login Button */}
          <button
            type="submit"
            className="bg-stone-950 text-white rounded-md py-2 px-4 w-full"
          >
            Login
          </button>
        </form>
      </div>
    </div>
  );
}

export default LoginPage;