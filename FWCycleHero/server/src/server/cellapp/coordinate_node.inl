/*
This source file is part of KBEngine
For the latest info, see http://www.kbengine.org/

Copyright (c) 2008-2016 KBEngine.

KBEngine is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

KBEngine is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.
 
You should have received a copy of the GNU Lesser General Public License
along with KBEngine.  If not, see <http://www.gnu.org/licenses/>.
*/


namespace KBEngine{

//-------------------------------------------------------------------------------------
INLINE CoordinateNode* CoordinateNode::pPrevX() const { return pPrevX_; }
INLINE CoordinateNode* CoordinateNode::pNextX() const { return pNextX_; }
INLINE CoordinateNode* CoordinateNode::pPrevY() const { return pPrevY_; }
INLINE CoordinateNode* CoordinateNode::pNextY() const { return pNextY_; }
INLINE CoordinateNode* CoordinateNode::pPrevZ() const { return pPrevZ_; }
INLINE CoordinateNode* CoordinateNode::pNextZ() const { return pNextZ_; }

//-------------------------------------------------------------------------------------
INLINE void CoordinateNode::pPrevX(CoordinateNode* pNode){ if(pNode != NULL)KBE_ASSERT(pPrevX_ != pNode); pPrevX_ = pNode; }
INLINE void CoordinateNode::pNextX(CoordinateNode* pNode){ if(pNode != NULL)KBE_ASSERT(pNextX_ != pNode); pNextX_ = pNode; }
INLINE void CoordinateNode::pPrevY(CoordinateNode* pNode){ if(pNode != NULL)KBE_ASSERT(pPrevY_ != pNode); pPrevY_ = pNode; }
INLINE void CoordinateNode::pNextY(CoordinateNode* pNode){ if(pNode != NULL)KBE_ASSERT(pNextY_ != pNode); pNextY_ = pNode; }
INLINE void CoordinateNode::pPrevZ(CoordinateNode* pNode){ if(pNode != NULL)KBE_ASSERT(pPrevZ_ != pNode); pPrevZ_ = pNode; }
INLINE void CoordinateNode::pNextZ(CoordinateNode* pNode){ if(pNode != NULL)KBE_ASSERT(pNextZ_ != pNode); pNextZ_ = pNode; }

//-------------------------------------------------------------------------------------
INLINE void CoordinateNode::pCoordinateSystem(CoordinateSystem* p){ pCoordinateSystem_ = p; }

//-------------------------------------------------------------------------------------
INLINE void CoordinateNode::flags(uint32 v){ flags_ = v; }
	
//-------------------------------------------------------------------------------------
INLINE uint32 CoordinateNode::flags() const{ return flags_; }
	
//-------------------------------------------------------------------------------------
INLINE CoordinateSystem* CoordinateNode::pCoordinateSystem() const{ return pCoordinateSystem_; }

//-------------------------------------------------------------------------------------
}
