package june;
import java.io.*;

public class Log {
    static BufferedWriter bf;
    static{
        try{
            bf = new BufferedWriter(new FileWriter("./CodeSpellsJava.log",true));
        }catch(IOException e){
            e.printStackTrace();
        }
    }
    
    public static void log(String message) {
        try {
            bf.write("Java: " + message+"\n");
            bf.flush();
        }
        catch (Exception e) {
            System.out.println("found an exception");
        }
    }




}
