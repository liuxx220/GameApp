/*
----------------------------------------------------------------------------------------------------
		file name : 
		desc      : ³¡¾°·þÎñÆ÷
		author	  : ljp

		log		  : by ljp create 2017-06-11
----------------------------------------------------------------------------------------------------
*/

#ifndef KBE_CELLS_H
#define KBE_CELLS_H

// common include
#include "cell.h"
#include "helper/debug_helper.hpp"
#include "common/common.hpp"


namespace KBEngine{

class Cells
{
public:
	Cells();
	~Cells();

	ArraySize size() const{ return cells_.size(); }

private:
	std::map<CELL_ID, Cell> cells_;
};

}
#endif
