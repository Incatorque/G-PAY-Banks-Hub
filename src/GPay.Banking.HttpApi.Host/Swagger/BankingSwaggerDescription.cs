namespace GPay.Banking.Swagger;

/// <summary>
/// HTML content shown at the top of Swagger UI (OpenAPI <c>info.description</c>).
/// </summary>
internal static class BankingSwaggerDescription
{
    /// <summary>
    /// Builds the description. When <paramref name="requireGpayAuth"/> is false,
    /// JWT guidance is shown as optional / disabled for local use.
    /// </summary>
    public static string Build(bool requireGpayAuth = true)
    {
        var authBlock = requireGpayAuth
            ? """
              <div class="gpay-callout gpay-callout-auth">
                <strong>Authentication</strong>
                <ul>
                  <li><strong>JWT Bearer</strong> — click <em>Authorize</em> and paste a token. Audience <code>GPay</code>.</li>
                  <li>Permissions: <code>Banking.Avs.*</code>, <code>Banking.Payments.*</code>, <code>Banking.TransactionHistory.View</code>.</li>
                  <li><strong>Callbacks</strong> stay anonymous — bank <code>Token</code> + optional IP/domain allow-list (not JWT).</li>
                </ul>
              </div>
              """
            : """
              <div class="gpay-callout gpay-callout-warn">
                <strong>Authentication disabled</strong>
                <p style="margin:0.4em 0 0;">
                  <code>AuthServer:RequireGpayAuth</code> is <strong>false</strong>.
                  JWT is not required for AVS / payments / transaction history.
                  Callbacks still use bank token + allow-list checks.
                </p>
              </div>
              """;

        return $$"""
            <div class="gpay-swagger-intro">
              <p class="gpay-lede">
                Bank-agnostic HTTP API for <strong>account verification (AVS)</strong>,
                <strong>instant payments</strong> (PayShap / RTC),
                <strong>transaction history</strong>, and <strong>bank payment callbacks</strong>.
              </p>

              {{authBlock}}

              <h3 class="gpay-h">Banks</h3>
              <p>
                Pass <code>bank</code> on the body (or query for callbacks):
                <span class="gpay-pill">Absa</span>
                <span class="gpay-pill">Fnb</span>
                <span class="gpay-pill">Nedbank</span>
              </p>
              <p class="gpay-muted">Absa is fully wired today; other codes may return not-supported until adapters are enabled.</p>

              <h3 class="gpay-h">Routes</h3>
              <p class="gpay-muted">ABP conventional controllers: <code>/api/app/{service-kebab}/{action-kebab}</code></p>
              <table class="gpay-table">
                <thead>
                  <tr><th>Area</th><th>Base path</th></tr>
                </thead>
                <tbody>
                  <tr><td>AVS</td><td><code>/api/app/account-verification</code></td></tr>
                  <tr><td>Instant payments</td><td><code>/api/app/instant-payment</code></td></tr>
                  <tr><td>Transaction history</td><td><code>/api/app/transaction-history</code></td></tr>
                  <tr><td>Callbacks</td><td><code>/api/app/callback</code></td></tr>
                </tbody>
              </table>

              <h3 class="gpay-h">Correlation &amp; persistence</h3>
              <p>
                Each outbound bank call allocates a <code>correlationId</code>.
                Successful AVS / payment calls persist a <code>BankHub*</code> record;
                responses include <code>recordId</code> for later lookup.
              </p>

              <h3 class="gpay-h">Callbacks</h3>
              <p>Register with Absa CAPI as:</p>
              <pre class="gpay-pre">POST https://{host}/api/app/callback/process-payment?bank=Absa</pre>
              <ul>
                <li>Body is <strong>bank-native</strong> Absa PaymentCallback JSON (includes shared <code>Token</code>).</li>
                <li>Absa may retry on failure — handlers are idempotent. After retries, Absa emails <code>SupportEmail</code>.</li>
                <li>Fallback status: <code>POST /api/app/instant-payment/get-status</code>.</li>
              </ul>

              <h3 class="gpay-h">Errors</h3>
              <ul>
                <li>Validation / business → ABP problem details (typically <code>400</code> / <code>403</code>).</li>
                <li>Missing or invalid JWT → <code>401</code> (when auth is required).</li>
                <li>Missing permission → <code>403</code>.</li>
              </ul>
            </div>
            """;
    }

    /// <summary>CSS injected into Swagger UI for the intro block.</summary>
    public const string Styles = """
        <style>
          .swagger-ui .info .title { font-weight: 650; letter-spacing: -0.02em; }
          .swagger-ui .info .description,
          .swagger-ui .info .description .renderedMarkdown { max-width: 52rem; }
          .gpay-swagger-intro { font-size: 14px; line-height: 1.55; color: #3b4151; }
          .gpay-lede { font-size: 15px; margin: 0 0 1rem; }
          .gpay-h {
            margin: 1.25rem 0 0.5rem;
            font-size: 13px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 0.04em;
            color: #1b1b1b;
            border-bottom: 1px solid #ebebeb;
            padding-bottom: 0.35rem;
          }
          .gpay-muted { color: #6b7280; font-size: 13px; margin: 0.35rem 0 0.75rem; }
          .gpay-callout {
            border-radius: 8px;
            padding: 0.85rem 1rem;
            margin: 0.75rem 0 1rem;
            border: 1px solid #dbe3f0;
            background: #f4f7fb;
          }
          .gpay-callout-auth { border-color: #c5d4f0; background: #f0f5ff; }
          .gpay-callout-warn { border-color: #f0d9a8; background: #fff8eb; }
          .gpay-callout ul { margin: 0.5rem 0 0; padding-left: 1.2rem; }
          .gpay-callout li { margin: 0.25rem 0; }
          .gpay-pill {
            display: inline-block;
            padding: 0.15rem 0.55rem;
            margin: 0 0.2rem 0.2rem 0;
            border-radius: 999px;
            background: #eef2f7;
            border: 1px solid #d9e1ec;
            font-size: 12px;
            font-family: monospace;
          }
          .gpay-table {
            width: 100%;
            border-collapse: collapse;
            margin: 0.5rem 0 0.75rem;
            font-size: 13px;
          }
          .gpay-table th,
          .gpay-table td {
            text-align: left;
            padding: 0.55rem 0.7rem;
            border-bottom: 1px solid #eceff3;
            vertical-align: top;
          }
          .gpay-table thead th {
            background: #f7f8fa;
            font-weight: 650;
            color: #374151;
          }
          .gpay-table tbody tr:hover td { background: #fafbfc; }
          .gpay-pre {
            margin: 0.4rem 0 0.75rem;
            padding: 0.75rem 0.9rem;
            background: #1e2430;
            color: #e8eef7;
            border-radius: 8px;
            font-size: 12.5px;
            overflow-x: auto;
            font-family: ui-monospace, SFMono-Regular, Menlo, Consolas, monospace;
          }
          .gpay-swagger-intro code {
            font-size: 12.5px;
            padding: 0.1rem 0.35rem;
            border-radius: 4px;
            background: #eef1f5;
          }
          .gpay-pre code { background: transparent; padding: 0; color: inherit; }
        </style>
        """;
}
