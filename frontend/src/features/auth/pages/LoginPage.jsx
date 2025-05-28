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
      <div class="sm:mx-start sm:w-full sm:max-w-sm">
        <img class="mx-start h-25 w-auto" src="https://the-ice.org/wp-content/uploads/2020/02/SIT-logo.png" alt="Your Company"></img>
      </div>
        <h1 className="text-2xl font-semibold my-4 font-inter-tight">Welcome to Ready4Work</h1>

        {/* Right: Notice */}
        <div className="relative w-full mx-auto border border-[#D3D3D3] rounded-2xl bg-white px-6 py-5 mb-6">
          <button
            type="button"
            aria-label="Close notice"
            className="absolute top-4 right-4 p-1 rounded hover:bg-gray-100 transition"
            // onClick={() => ...} // add handler if you want to dismiss
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-6 w-6 text-black"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
              strokeWidth={2}
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M6 18L18 6M6 6l12 12"
              />
            </svg>
          </button>
          <h5 className="font-inter-tight font-medium text-lg text-black mb-2">
            Notice
          </h5>
          <p className="font-inter-tight text-sm text-black">
            Access to Ready4Work will be restricted from 10:00 PM to 6:00 AM on weekdays to support students’ well-being and promote a healthy work-life balance.
          </p>
        </div>
        <form action="#" method="POST">
          {/* Username Input */}
          <div className="relative bg-white rounded-lg border border-[#D3D3D3] focus-within:border-[#7a7a7a] px-3 py-3 font-inter-tight transition-colors group mb-4">
            <label
              htmlFor="username"
              className="block text-xs font-normal text-[#B0B0B0] group-focus-within:text-[#7a7a7a]"
            >
              Username
            </label>
            <input
              type="text"
              name="username"
              id="username"
              className="w-full bg-transparent text-sm focus:outline-none font-inter-tight"
              autoComplete="username"
            />
          </div>

          {/* Password Input */}
          <div className="relative bg-white rounded-lg border border-[#D3D3D3] focus-within:border-[#7a7a7a] px-3 py-3 font-inter-tight transition-colors group">
            <label
              htmlFor="password"
              className="block text-xs font-normal text-[#B0B0B0] group-focus-within:text-[#7a7a7a]"
            >
              Password
            </label>
            <input
              type="password"
              name="password"
              id="password"
              className="w-full bg-transparent text-sm focus:outline-none font-inter-tight"
              autoComplete="current-password"
            />
          </div>

          <div className="mt-8 mb-8 flex items-center justify-between font-inter-tight">
            {/* Remember Me Checkbox */}
            <label className="flex items-center gap-2">
              <input
                type="checkbox"
                name="remember"
                className="accent-black"
              />
              <span className="text-sm text-b0 font-inter-tight">Remember me</span>
            </label>

            {/* Forgot Password Link */}  
            <a className="text-sm font-medium underline text-[#E30613] transition" href="#">
              Forgot password?
            </a>
          </div>
          
          {/* Login Button */}
          <button type="submit" className="bg-stone-950 text-white rounded-md py-3 px-4 w-full font-inter-tight">
            Login
          </button>
        </form>
      </div>
    </div>
  );
}

export default LoginPage;