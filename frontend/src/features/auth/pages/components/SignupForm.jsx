import StatusInputField from '../../../../components/StatusInputField';
import PrimaryButton from '../../../../components/PrimaryButton';
import NoticeBanner from '../../../../components/NoticeBanner';
import DisclaimerCheckbox from '../../../../components/DisclaimerCheckbox';

export default function SignupForm() {
  return (
    <form action="#" method="POST">
      <StatusInputField
        label="Username"
        name="Username"
        type="text"
        status="default"
        errorMessage=""
      />
      <StatusInputField
        label="Password"
        name="password"
        type="password"
        status="default"
        errorMessage=""
      />
    <StatusInputField
        label="Reverify Password"
        name="password"
        type="password"
        status="error"
        errorMessage="Passwords do not match"
      />
    <div className="mt-6">
        <NoticeBanner
            title="Student Account Detected"
            message="We’ll set up your student account using the details you’ve provided. Need help? Reach out to the SIT Helpdesk."
            onClose={() => setShowNotice(false)}
        />
    </div>
    <DisclaimerCheckbox 
    title="Student Account" 
    description="Create Ready4Work account with SIT Credentials"
    name="studentAccount"
    />

      
      <PrimaryButton type="submit" label="Create Account" />
    </form>
  );
}
