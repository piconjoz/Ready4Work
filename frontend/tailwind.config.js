// tailwind.config.js
module.exports = {
  content: [
    "./public/**/*.html",            // if you have any plain HTML
    "./src/**/*.{js,jsx,ts,tsx,vue}",// your React/Vue/etc. components
    "node_modules/flowbite/**/*.js"  // pull in Flowbite’s JS components
  ],
  theme: {
    extend: {},                       // your custom theme tweaks (if any)
  },
  plugins: [
    require("flowbite/plugin"),      // enable Flowbite’s plugin
  ],
};