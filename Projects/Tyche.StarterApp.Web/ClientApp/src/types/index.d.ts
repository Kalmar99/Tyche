export {};

declare global {
    interface Window {
        FB:any;
        __WB_MANIFEST: any,
        skipWaiting: () => any,
    }
}