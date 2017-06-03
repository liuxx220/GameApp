require "Common/define"


--管理器--
local transform;
local gameObject;
local _Slider 	= null;


CLoadLevelFrame  = {}
local this      = CLoadLevelFrame;


--初始化面板--
function CLoadLevelFrame.ReloadUI( obj )
	
	gameObject			= obj;
	transform			= gameObject.transform;
	warn("ReloadUI lua--->>"..gameObject.name );
	
    _Slider     		= transform:Find("Anchor/Progress Bar"):GetComponent("UISlider");
	
	--local goo = go.transform:FindChild('Label');
    --goo:GetComponent('UILabel').text = i;
end



function CLoadLevelFrame.OnDestroy()

	warn("OnDestroy---->>>");
	_Slider    			= null;
    
end


function CLoadLevelFrame.Update()
	
	local nprocess = LevelManager:GetLoadProcess();
	if _Slider then 
		_Slider.value 	 = toProgress;
	end
	
end

	

