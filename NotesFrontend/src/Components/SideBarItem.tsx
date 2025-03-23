import React from "react";

interface SideBarItemProps {
  icon?: string;
  text: string;
  onClick: Function;
}

const SideBarItem: React.FC<SideBarItemProps> = ({ icon, text, onClick }) => {
  return (
    <li
      className="hover:bg-blue-100 rounded-md pl-3 py-3 cursor-pointer relative"
      onClick={() => onClick()}
    >
      <i className={`${icon} mr-2 text-blue-950`}></i>
      <span className="text-[0.9rem]">{text}</span>
    </li>
  );
};

export default SideBarItem;
