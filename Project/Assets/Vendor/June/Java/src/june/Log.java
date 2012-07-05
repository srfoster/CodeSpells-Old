package june;
import java.io.BufferedWriter;
import java.io.FileWriter;

public class Log {
    
    public static void log(String message) {
        try {
            BufferedWriter bf = new BufferedWriter(new FileWriter("/Codespells/Codespells/ConsoleOutput.txt",true));
            bf.write(message+"\n");
            bf.flush();
            bf.close();
        }
        catch (Exception e) {
            System.out.println("found an exception");
        }
    }




}