package june;

import java.util.Observer;
import java.util.Observable;

class SigHandler
extends Observable
implements sun.misc.SignalHandler
{
     public void handleSignal( final String signalName ) throws IllegalArgumentException {
         try {
            sun.misc.Signal.handle( new sun.misc.Signal(signalName), this );
         }
         catch( IllegalArgumentException x ) {
            // Most likely this is a signal that's not supported on this
             // platform or with the JVM as it is currently configured
             throw x;
         }
         catch( Throwable x ) {
             // We may have a serious problem, including missing classes
             // or changed APIs
             throw new IllegalArgumentException( "Signal unsupported: "+signalName, x );
         }
     }

     public void handle( final sun.misc.Signal signal )
     {
         // setChanged ensures that notifyObservers actually calls someone. In
         // simple cases this seems like extra work but in asynchronous designs,
         // setChanged might be called on one thread, and notifyObservers, on
         // another or only when multiple changes may have been completed (to
         // wrap up multiple changes in a single notifcation).
        setChanged();
        notifyObservers( signal );
     }
}