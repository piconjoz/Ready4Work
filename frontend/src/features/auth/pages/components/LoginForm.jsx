import StatusInputField from '../../../../components/StatusInputField';
import PrimaryButton from '../../../../components/PrimaryButton';

export default function LoginForm() {
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
      <div className="mt-8 mb-8 flex items-center justify-between">
        <label className="flex items-center gap-2">
          <input type="checkbox" name="remember" className="accent-black" />
          <span className="text-sm text-b0 ">Remember me</span>
        </label>
        <a className="text-sm font-medium underline text-[#E30613] transition" href="#">
          Forgot password?
        </a>
      </div>
      <PrimaryButton type="submit" label="Login" />
    </form>
  );
}
