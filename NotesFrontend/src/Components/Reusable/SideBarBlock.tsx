import React, { ReactNode } from "react";

interface SideBarBlockProps {
  title?: string;
  border?: boolean;
  children: ReactNode;
  ctnStyle?: string;
}

const SideBarBlock: React.FC<SideBarBlockProps> = ({
  title,
  children,
  border,
  ctnStyle,
}) => {
  return (
    <div className={`py-2 px-3 max-h-full relative ${ctnStyle}`}>
      <h2 className="text-xs text-gray-500 py-1 mb-2">{title}</h2>
      <ul>{children}</ul>
      {border && (
        <div className="absolute bottom-0 left-3 right-3 h-[1px] bg-gray-200"></div>
      )}
    </div>
  );
};

export default SideBarBlock;
