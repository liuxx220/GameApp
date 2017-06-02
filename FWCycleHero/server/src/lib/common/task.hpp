/*
-----------------------------------------------------------------------------
		file name : task.hpp
		desc      : 抽象一个任务
		author    : ljp

		log       : create by ljp [ 2015-11-22 ]
-----------------------------------------------------------------------------
*/

#ifndef KBE_TASK_HPP
#define KBE_TASK_HPP
#include "common/common.hpp"






namespace KBEngine
{

	/**
	 *	抽象一个任务
	 */
	class Task
	{
	public:
		virtual ~Task() {}
		virtual bool process() = 0;
	};
}

#endif // KBE_TASK_HPP
