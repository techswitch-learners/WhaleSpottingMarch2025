import { useState } from "react";
import CustomButton from "../../formComponents/customButton/CustomButton";

export const Admin = () => {
  const [count, setCount] = useState(0);

  return (
    <div>
      <div>Admin page</div>
      <CustomButton onClick={() => setCount((count) => count + 1)}>
        count is {count}
      </CustomButton>
    </div>
  );
};