import StatusInputField from "../../../../components/StatusInputField";

export default function JobInfo() { 
    return (
        <>
        <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6">
            <h2 className="text-xl font-semibold mb-4">Job Requirements</h2>
            <div className="space-y-4">
                <StatusInputField
                    label="Job Requirements"
                    name="jobRequirements"
                    type="textarea"
                    status="default"
                    defaultValue="	Bachelor of Food Technology (Hons) or equivalent (final-year students may apply)
	•	Knowledge of food formulation, processing techniques, and ingredient functionality
	•	Familiarity with food safety and quality standards (e.g. HACCP, GMP, ISO 22000)
	•	Hands-on experience in food product trials, sensory testing, or lab-based coursework
	•	Strong analytical and problem-solving skills
	•	Proficient in MS Office (especially Excel) for data recording and reporting
	•	Good teamwork, communication, and documentation skills
	•	Passionate about food innovation and staying updated with industry trends"
                    readOnly={false}
                    // className="min-h-[50vh] lg:min-h-[40vh]"
                />
            </div>
        </div>
        {/* Job Description Section */}
        <div className="bg-white border border-[#D3D3D3] rounded-2xl p-6 mt-6">
          <h2 className="text-xl font-semibold mb-4">Job Description</h2>
          <div className="space-y-4">
            <StatusInputField
              label="Job Description"
              name="jobDescription"
              type="textarea"
              status="default"
              defaultValue={`We are looking for an enthusiastic and innovative Product Development Technologist to join our R&D team. This role is ideal for recent graduates or final-year students with a strong foundation in food science and technology. You will assist in the development, testing, and optimization of new food products, from concept to launch.

You will be involved in formulating recipes, conducting sensory and shelf-life evaluations, and collaborating with cross-functional teams including marketing, production, and quality assurance. This is an excellent opportunity to apply your academic knowledge to real-world product development in a fast-paced food manufacturing environment.`}
              readOnly={false}
            //   className="min-h-[50vh] lg:min-h-[40vh]"
            />
          </div>
        </div>
        </>
    );
}