import React, { ReactNode } from "react";

interface SideBarBlockProps {
  title?: string;
  children: ReactNode;
}

const SideBarBlock: React.FC<SideBarBlockProps> = ({ title, children }) => {
  return (
    <div className="p-3">
      <h2 className="text-sm text-gray-700 px-2 py-1">{title}</h2>
      <ul>{children}</ul>
    </div>
  );
};

export default SideBarBlock;
