const Header = () => {
  return (
    <div className="flex min-w-screen px-3 py-5 justify-between shadow">
      <h1 className="text-blue-950 text-xl font-bold">Notoria</h1>
      <div className="flex gap-3">
        <div>language</div>
        <div>settings</div>
      </div>
    </div>
  );
};

export default Header;
