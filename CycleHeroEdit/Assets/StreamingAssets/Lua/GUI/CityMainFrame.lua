require "Common/define"


--管理器--
local transform;
local gameObject;
local LuaBehaviour;




CityMainFrame     = {}
local this        = CityMainFrame;


local			m_btnRole;
local			m_btnBag;
local			m_btnSkill;
local 			m_btnNpcnav;
	
--初始化面板--
function CityMainFrame.ReloadUI( obj )
	
	gameObject			= obj;
	transform			= gameObject.transform;
	warn("ReloadUI lua--->>"..gameObject.name );
	
    m_btnRole   		= transform:FindChild("HeroInfo/Meunbar/Character").gameObject;
    m_btnBag    		= transform:FindChild("HeroInfo/Meunbar/bag").gameObject;
	m_btnSkill    		= transform:FindChild("HeroInfo/Meunbar/skill").gameObject;
	m_btnNpcnav    		= transform:FindChild("QuestNav/NavBtn").gameObject;
	
	
	--warn("ReloadUI lua--->>"..btnEnterGame.name );
    
	LuaBehaviour 	 	= transform:GetComponent('CLuaBehaviour');
	LuaBehaviour:AddClick(m_btnRole, this.ClickRole);
	LuaBehaviour:AddClick(m_btnBag, this.ClickBag);
	LuaBehaviour:AddClick(m_btnSkill, this.ClickSkill);
	LuaBehaviour:AddClick(m_btnNpcnav, this.ClickQuestNav);
 
	--local goo = go.transform:FindChild('Label');
    --goo:GetComponent('UILabel').text = i;
end

--单击事件--
function CityMainFrame.OnDestroy()

	warn("OnDestroy---->>>");
	m_btnRole	= null;
	m_btnBag	= null;
	m_btnSkill	= null;	
	m_btnNpcnav	= null;
	
end


function CityMainFrame.ClickRole( obj )

	
end

function CityMainFrame.ClickBag( obj )

	
end

function CityMainFrame.ClickSkill( obj )

	
end


function CityMainFrame.ClickQuestNav( obj )

	
end


function CityMainFrame.OnCreateFrame( )

	warn('CityMainFrame Create OK--->>>');
	
end
	
	

