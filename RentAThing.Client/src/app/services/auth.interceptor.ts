import { HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { AuthService } from "./auth.service";
import { inject } from "@angular/core";
import { from, switchMap } from "rxjs";
import { LoggingService } from "./logging.service";

export function authInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn) {
    // Inject the current `AuthService` and use it to get an authentication token:
    const authService = inject(AuthService);
    const logger = inject(LoggingService);

    return next(req);
    //const token = await authService.getToken();
    if (!authService.isLoggedIn()) {
        logger.debug('[authInterceptor] not logged in', req);
        return next(req);
    }

    //(alias) from<Promise<AuthenticationResult>>(input: Promise<AuthenticationResult>): Observable<AuthenticationResult> (+1 overload)

    // return from(
    //     (async () => {
    //         // Ensure AuthService is initialized.
    //         logger.debug('[authInterceptor] get token');
    //         await authService.init();

    //         // Acquire a token silently.
    //         const token = await authService.acquireTokenSilent();
    //         return token;
    //     })()
    // ).pipe(
    //     switchMap((tokenResponse) => {
    //         const token = tokenResponse.accessToken;

    //         // Clone the request and set the Authorization header
    //         const authReq = req.clone({
    //             setHeaders: {
    //                 Authorization: `Bearer ${token}`,
    //             },
    //         });

    //         return next(authReq);
    //     })
    // );
}