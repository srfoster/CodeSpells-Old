
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;

import java.net.Socket;

public class SocketTest
{
	public static void main(String[] args) throws Exception
	{
		Socket soc = new Socket("127.0.0.1",3000);
		PrintWriter out = new PrintWriter(soc.getOutputStream(), true);
		BufferedReader in = new BufferedReader(new InputStreamReader(soc.getInputStream()));
		BufferedReader sin = new BufferedReader(new InputStreamReader(System.in));

		String fromUser;
		System.out.print("Client: ");
		while ((fromUser = sin.readLine()) != null) {
			out.println(fromUser);

			String fromServer = in.readLine();

			System.out.println("Server: " + fromServer);
			if (fromServer.equals("Bye."))
			        break;

			System.out.print("Client: ");
		}
	}
}
