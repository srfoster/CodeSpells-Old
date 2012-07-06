package june;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;

import java.net.Socket;


public class UnityConnection
{
    static PrintWriter out;
    static Socket soc;
    static BufferedReader in;

    static{
        try{
          soc = new Socket("127.0.0.1",3000);
          out = new PrintWriter(soc.getOutputStream(), true);
          in = new BufferedReader(new InputStreamReader(soc.getInputStream()));
        }catch(Exception e){
          e.printStackTrace();
          //Should also probably tell Unity that there's been a problem...
        }
    }

    public static PrintWriter getOutgoingWriter()
    {
      return out;
    }

    public static BufferedReader getIncomingReader()
    {
      return in;
    }
}
