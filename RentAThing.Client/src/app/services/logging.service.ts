import { inject, Injectable, InjectionToken } from '@angular/core';

export const LoggingServiceConfigToken = new InjectionToken<LoggingServiceConfig>('LoggingServiceConfig');

export enum LogLevel {
    Debug,
    Info,
    Warn,
    Error,
    Off,
}

export interface LoggingServiceConfig {
    logLevel: LogLevel;
    consoleLogOnly?: boolean;
}

const defaultLoggingServiceConfig: LoggingServiceConfig = {
    logLevel: LogLevel.Debug,
    consoleLogOnly: true,
};

export const provideLoggingServiceConfig = (config: LoggingServiceConfig) => {
    return { provide: LoggingServiceConfigToken, useValue: config };
}

@Injectable({
    providedIn: 'root',
})
export class LoggingService {

    logConfig = inject(LoggingServiceConfigToken, { optional: true }) || defaultLoggingServiceConfig;

    constructor() {
        console.log('[LoggingService ctor]', this.logConfig);
        // this.debug('LoggingService sample debug message', { test: 'test' });
        // this.info('LoggingService sample info message', { test: 'test' });
        // this.warn('LoggingService sample warn message', { test: 'test' });
        // this.error('LoggingService sample error message', { test: 'test' });
    }

    debug(message: string, ...optionalParams: any[]): void {
        if (this.logConfig.logLevel <= LogLevel.Debug) {
            if (this.logConfig.consoleLogOnly)
                console.log(`[DEBUG]: ${message}`, ...optionalParams);
            else
                console.debug(`[DEBUG]: ${message}`, ...optionalParams);
        }
    }

    info(message: string, ...optionalParams: any[]): void {
        if (this.logConfig.logLevel <= LogLevel.Info) {
            if (this.logConfig.consoleLogOnly)
                console.log(`[INFO]: ${message}`, ...optionalParams);
            else
                console.info(`[INFO]: ${message}`, ...optionalParams);
        }
    }

    warn(message: string, ...optionalParams: any[]): void {
        if (this.logConfig.logLevel <= LogLevel.Warn) {
            if (this.logConfig.consoleLogOnly)
                console.log(`[WARN]: ${message}`, ...optionalParams);
            else
                console.warn(`[WARN]: ${message}`, ...optionalParams);
        }
    }

    error(message: string, ...optionalParams: any[]): void {
        if (this.logConfig.logLevel <= LogLevel.Error) {
            if (this.logConfig.consoleLogOnly)
                console.log(`[ERROR]: ${message}`, ...optionalParams);
            else
                console.error(`[ERROR]: ${message}`, ...optionalParams);
        }
    }

}
