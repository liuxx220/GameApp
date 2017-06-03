require "Common/define"
require "Common/Global"


local Time 				= UnityEngine.Time;

--管理器--
local transform;
local gameObject;
local LuaBehaviour;




CRoleSelectFrame    	= {}
local this      		= CRoleSelectFrame;
local myCamera			= null;
local RenderTarget      = null;
local m_Players			= {};


local m_PlayerCount 	= 5;
local m_currentIndex	= 0;
local MARGIN_X			= 3;
local ITEM_WITH			= 8;
local _touchidstance	= 0;
local sliderValue   	= 2;

local FMD_LEFT			= 0;
local FMD_STOP			= 1;
local FMD_RIGHT			= 2;
local _touchMoveDir     = 1;

local m_ClassInfo		= null;
local m_ClassName		= null;
local m_StartGame		= null;
local m_bg				= null;


--初始化面板--

function CRoleSelectFrame.ReloadUI( obj )
	
	gameObject			= obj;
	transform			= gameObject.transform;
	warn("ReloadUI lua--->>"..gameObject.name );

    m_StartGame     	= transform:FindChild("Anchor/heromiaoshu/startgame").gameObject;
    --m_ClassName     	= transform:FindChild("Anchor/heromiaoshu/bg4/classname"):GetComponent('UILabel');
    --m_ClassInfo     	= transform:FindChild("Anchor/heromiaoshu/bg3/classinfo"):GetComponent('UILabel');
    --m_bg            	= transform:FindChild("Anchor/heromiaoshu/bg2" ):GetComponent('UITexture');
       
	LuaBehaviour 	 	= transform:GetComponent('CLuaBehaviour');
	LuaBehaviour:AddClick(m_StartGame, this.ClickStartGame);
	
	
	--RenderTarget		= UnityEngine.RenderTexture.New( 1024, 512, 24 );
	--myCamera 			= UnityEngine.GameObject.Find("MainCamera"):GetComponent('Camera');
	--if myCamera then
	--	myCamera.targetTexture = RenderTarget;
	--	m_bg.mainTexture= RenderTarget;
	--end
	
	--this.InitModel();
end


--初始化要显示的模型--
function CRoleSelectFrame.InitModel()
	
	for i = 0, 4 do
		 this.GeneratePlayer( i );
	end
		
	this.moveSlider( 3 );
end


function CRoleSelectFrame.moveSlider( id )

	if m_currentIndex ~= id then
	
		m_currentIndex = id;
		for i=0, 4 do
		
			local targetX   = 0;
            local targetRot = 0;
			
			targetX = MARGIN_X * (i - id);
			warn("targetX"..targetX );
			--left slides
            if i < id then 
            
                targetX 	= targetX - ITEM_WITH * 0.6;
                targetRot 	= -60;
			
            --right slides
            elseif i > id then
			
                targetX 	= targetX + ITEM_WITH * 0.6;
                targetRot 	= 60;
            
            else
            
                targetX 	= 0;
                targetRot 	= 0;
            end
			
			
			local pPlayer 	= m_Players[i];
			local ys    	= pPlayer.transform.position.y;
			local zs		= pPlayer.transform.position.z;
			
			warn("player"..i .." x="..targetX .."y ="..ys .. " z=".. zs);
			local ea  		= pPlayer.transform.eulerAngles;

			iTween.MoveTo(pPlayer, Vector3.New(targetX, ys, zs), 1);
			iTween.RotateTo(pPlayer, Vector3.New(ea.x, ea.y, targetRot), 1);
		end
	end
end


function CRoleSelectFrame.Update()
	--[[
	if Input.touchCount  > 0 then 
		if Input.GetTouch(0).phase == TouchPhase.Moved  then
		
			if Input.GetTouch(0).deltaPosition.x < 0.01 then
				_touchMoveDir = FMD_LEFT;
			else
				_touchMoveDir = FMD_RIGHT;
			end
		end

		if Input.GetTouch(0).phase == TouchPhase.Stationary then 
			_touchMoveDir = FMD_STOP;
		
		end
	end
	
	if _touchMoveDir ~= FMD_STOP then 
        
		if _touchMoveDir == FIMOVEDIR.FMD_Left then
			_touchdistance = _touchdistance + Time.deltaTime;
			if _touchdistance > 0.5 then
				sliderValue = sliderValue + 1;
				if sliderValue >= m_PlayerCount then
					sliderValue = m_PlayerCount - 1;
				end
				moveSlider(sliderValue);
			end
		else
			_touchdistance = _touchdistance + Time.deltaTime;
			if _touchdistance > 0.5 then 
				sliderValue = sliderValue - 1;
				if sliderValue < 0 then
					sliderValue = 0;
				end
				moveSlider(sliderValue);
			end
		end
    end
	--]]
end


--单击事件--
function CRoleSelectFrame.OnDestroy()

	warn("OnDestroy---->>>");
	m_ClassInfo		= null;
	m_ClassName		= null;
	m_StartGame		= null;
	m_bg			= null;
	
end


function CRoleSelectFrame.ClickStartGame( obj )

	PanelManager:DestoryFrame("CLoginFrame");
	LevelManager:LoadLevel("City");
end
	

function CRoleSelectFrame.GeneratePlayer( id )
    
	local perfab  = "Player" .. (id+1);
    local player  = CBundleManager:CreateObjByBundle( perfab, 2 );
    m_Players[id] = player;    
end
