using System;

public class Message  : IMessage
{

    public Message( string name ) : this( name, null, null )
    {

    }

    public Message( string name, object body ) : this( name, body, null )
    {

    }

    public Message( string name, object body, string type )
    {
        m_name      = name;
        m_body      = body;
        m_type      = type;
    }

    public virtual string Name 
    {
        get { return m_name; }
    }

    public virtual object Body 
    {
        get
        {
            return m_body;
        }
        set
        {
            m_body = value;
        }
    }

    public virtual string Type 
    {
        get
        {
            return m_type;
        }

        set
        {
            m_type = value;
        }
    }

    public override string ToString()
    {
        string msg  = "Notification Name : " + Name;
               msg += "\nBody: " + ((Body == null ) ? "null" : Body.ToString() );
               msg += "\nType: " + ((Type == null ) ? "null" : Type.ToString());
               return msg;
    }


    /// <summary>
    /// The name of the notification instance 
    /// </summary>
    private string m_name;

    /// <summary>
    /// The type of the notification instance
    /// </summary>
    private string m_type;

    /// <summary>
    /// The body of the notification instance
    /// </summary>
    private object m_body;
}