import React from "react";

import Board from "../../components/Board/Board";

function MainPage(props) {
	return (
		<>
			<div className="flex h-screen justify-center items-center bg-gradient-to-br from-green-400 to-blue-500">
				<Board width={10} height={10}></Board>
			</div>
		</>
	);
}

export default MainPage;
