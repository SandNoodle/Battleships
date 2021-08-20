import HitIcon from "../Icons/HitIcon";
import MissIcon from "../Icons/MissIcon";
import SinkIcon from "../Icons/SinkIcon";

function BoardCell({ type }) {
	const CellStatus = (cellType) => {
		// Should be switched for ENUM
		switch (cellType) {
			case 0:
				return (
					<HitIcon className="w-9/12 h-9/12 fill-current text-black text-opacity-60" />
				);
			case 1:
				return (
					<SinkIcon className="w-9/12 h-9/12 fill-current text-black text-opacity-80" />
				);
			case 2:
				return (
					<MissIcon className="w-9/12 h-9/12 fill-current text-black text-opacity-25" />
				);
			default:
				return null;
		}
	};

	return (
		<>
			<div>
				<div className="flex md:w-16 md:h-16 sm:w-8 sm:h-8 w-6 h-6 m-1 sm:rounded-md md:rounded-md shadow-lg bg-gray-100 bg-opacity-25 border border-opacity-20 justify-center content-center">
					{CellStatus(type)}
				</div>
			</div>
		</>
	);
}

export default BoardCell;
