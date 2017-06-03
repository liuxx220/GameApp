--NPC10001
function OnTalk10001( )

  local nRet = Quest.IsCanAccepted( 1 );
  if nRet == 1 then
		
		Quest.OpenNpcTalk( 10001 );
		
		Quest.SetNpcTalkOption( 1, 0, 1, "欢迎来到封神世界!" );
  end
  
end


function OnAcceptQuestTalk10001()
  
end


function OnCompleteQuestTalk10001(id, step)
  
end


function OnScenarioTalk10001( id, step )
	
	if id == 1 then
		
		if step == 0 then 
			Quest.SetNpcTalkOption( 1, 1, 1, "小英雄终于等到你了!" );
		end
		
		if step == 1 then
			Quest.SetNpcTalkOption( 1, 2, 0, "有什么事情，请村长告诉我!" );
		end
		
		if step == 2 then 
			Quest.SetNpcTalkOption( 1, 3, 1, "前几村里发生一些奇怪的事情，想请小英雄去查一查!" );
		end
		
		if step == 3 then 
			Quest.SetNpcTalkOption( 1, 4, 0, "你说吧，我这就去!" );
		end
		
		if step == 4 then
			Quest.CloseNpcTalk();
		end
		
	end
	
end





