import BoardCell from "./BoardCell";

function BoardCells({cellData}) {
	if(!cellData) {
		return null;
	}

	return cellData.map((cell) => {
		return(
			<BoardCell key={cell.id} type={cell.type} />
		)
	})
}

function Board(props) {

	// Temporary function to present 10x10 board with random cell statuses
	const FetchDefaultData = () => {
		const cellArray = [];
		for(let i = 0; i < props.width * props.height; i++) {
			// For simplicy sake (temporary) data assumses just and integer to be passed for cell type.
			cellArray.push({type: Math.floor(Math.random() * 10)}); 
		}
		return cellArray;
	};

	return (
		<>
			<div className="grid grid-cols-10 grid-rows-10 justify-center md:p-4 sm:p-4 p-2 rounded-lg bg-white shadow-2xl 
							bg-opacity-40 backdrop-filter backdrop-opacity-md backdrop-blur-md border border-opacity-20">
								<BoardCells cellData={FetchDefaultData()} />
				
			</div>
		</>
	);
}

export default Board;
