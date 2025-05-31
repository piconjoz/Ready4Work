import Header from "../../../components/Header";

export default function BasePage() {
  return (
      <div className="min-h-screen bg-[#F8F9FD] px-6 md:px-10">
          <Header active="applications" />
            {/* Recommended */}
          <div className="py-6">
            <h1 className="text-2xl font-semibold">Title Here</h1>
          </div>
      </div>
  );
}